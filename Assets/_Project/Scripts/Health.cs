using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int objectHealth;
    private int health;

    [Header("Respawn Point")] 
    [SerializeField] private GameObject respawnPoint;
    
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
                transform.position = respawnPoint.transform.position;
            }
            else
            {
                Destroy(gameObject);
            }
            
        }
    }
}
