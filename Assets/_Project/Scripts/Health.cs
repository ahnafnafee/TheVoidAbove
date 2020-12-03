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
                Rigidbody _rb = GetComponent<Rigidbody>();
                transform.position = respawnPoint.transform.position;
                _rb.velocity = Vector3.zero;
                _rb.angularVelocity = Vector3.zero;
            }
            else
            {
                var transform1 = enemyCenter.transform;
                Instantiate(explosionFx, transform1.position, transform1.rotation);
                Destroy(gameObject);
            }
            
        }
    }
}
