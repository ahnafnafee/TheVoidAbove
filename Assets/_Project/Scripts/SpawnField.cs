using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnField : MonoBehaviour
{
    [SerializeField] int asteroidNumber,seed;
    [SerializeField] GameObject[] asteroid = new GameObject[10];
    [SerializeField] int[] randomAsteroid;
    GameObject[] asteroidClones;
    [SerializeField] Vector3 spawnRange;

    void Start()
    {
        Random.InitState(seed);

        randomAsteroid = new int[asteroidNumber];
        asteroidClones = new GameObject[asteroidNumber];


        for (int i = 0; i < asteroidNumber; i++)
        {
            randomAsteroid[i] = Random.Range(0, 5);
            asteroidClones[i] = Instantiate(asteroid[randomAsteroid[i]], new Vector3(transform.position.x + Random.Range(-spawnRange.x, spawnRange.x), 
                                                                                     transform.position.y + Random.Range(-spawnRange.y, spawnRange.y),
                                                                                     transform.position.z + Random.Range(-spawnRange.z, spawnRange.z)),
                                                                                     Quaternion.identity);

            asteroidClones[i].transform.parent = this.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
