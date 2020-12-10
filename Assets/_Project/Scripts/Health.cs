using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    [Header("Health Pack")]
    [SerializeField]
    private GameObject healthPack;

    [SerializeField]
    private GameObject gameOverUI; 

    private IEnumerator coroutine;

    void Start()
    {
        health = objectHealth;
    }
    
    public void TakeDamage(int damage)
    {
        if ((objectHealth - damage) > 0)
        {
            objectHealth -= damage;
        }
        else
        {
            // TODO: UI notification that player died
            objectHealth = health;

            if (gameObject.CompareTag("Player"))
            {
                transform.Find("PlayerBase").gameObject.SetActive(false);   
                coroutine = gameOver(2);
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
                Instantiate(healthPack, transform1.position, Quaternion.identity);
                Destroy(gameObject);
            }
            
        }
    }

    private IEnumerator gameOver(float respawnTime)
    {
        yield return new WaitForSeconds(respawnTime);
        transform.Find("PlayerBase").gameObject.SetActive(true);
        Rigidbody _rb = GetComponent<Rigidbody>();
        transform.position = respawnPoint.transform.position;
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        gameOverUI.SetActive(false);
    }
}
