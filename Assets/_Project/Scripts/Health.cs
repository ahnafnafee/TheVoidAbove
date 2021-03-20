using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Utils;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] public float objectHealth;
    public float health { get; set; }

    [Header("Respawn Point")] 
    [SerializeField] private GameObject respawnPoint;
    [SerializeField] private GameObject explosionFx;
    [SerializeField] private GameObject enemyCenter;


    [Header("Power Ups")]
    [SerializeField] private GameObject healthPack;
    [FormerlySerializedAs("drop")] [SerializeField] private GameObject ammoDrop;

    [Header("UI")]
    [SerializeField]
    private GameObject gameOverUI;

    [Header("Camera Shake")]
    [SerializeField] private CameraShake cShake;
    [SerializeField] private float shakeDuration = 0.2f;
    [SerializeField] private float shakeAmplitude = 3f;

    private IEnumerator coroutine;
    private List<GameObject> drops;
    private readonly System.Random random = new System.Random();

    void Start()
    {
        health = objectHealth;
        drops = new List<GameObject>(new[]{ammoDrop, healthPack});
    }
    
    public void TakeDamage(int damage,bool check=true)
    {
        if ((objectHealth - damage) > 0)
        {
            if (check)
            {
                AkSoundEngine.PostEvent("bullet_impact_1_event", this.gameObject);
            }

            // Adding camera shakes here
            if (gameObject.CompareTag("Player"))
            {
                cShake.Shake(shakeAmplitude, shakeDuration);
            }
            objectHealth -= damage;
        }
        else
        {
            // TODO: UI notification that player died

            if (gameObject.CompareTag("Player"))
            {
                transform.Find("PlayerBase").gameObject.SetActive(false);   
                coroutine = GameOver(2);
                StartCoroutine(coroutine);
                Rigidbody _rb = GetComponent<Rigidbody>();
                _rb.velocity = Vector3.zero;
                _rb.angularVelocity = Vector3.zero;
                gameOverUI.SetActive(true);
            }
            else
            {
                var transform1 = enemyCenter.transform;
                Instantiate(explosionFx, transform1.position, transform1.rotation);
                
                var powerUp = drops[random.Next(drops.Count)];
                Instantiate(powerUp, transform1.position, Quaternion.identity);

                Debug.Log(gameObject.name);
                if (gameObject.CompareTag("Enemy"))
                {
                    AkSoundEngine.PostEvent("robot_death_event", gameObject);
                }
                Destroy(gameObject);

            }
            
        }
    }

    private IEnumerator GameOver(float respawnTime)
    {
        Debug.Log($"Health: {objectHealth}");
        yield return new WaitForSeconds(respawnTime);
        transform.Find("PlayerBase").gameObject.SetActive(true);
        Rigidbody _rb = GetComponent<Rigidbody>();
        transform.position = respawnPoint.transform.position;
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;

        objectHealth = health;

        gameOverUI.SetActive(false);
    }


    public void UpdateRespawnPoint(GameObject g) {
        respawnPoint = g;
    }
}
