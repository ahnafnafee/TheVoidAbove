using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomDebris : MonoBehaviour
{
    [SerializeField]
    GameObject debris;

    [SerializeField]
    float distance;

    [SerializeField]
    int maxDebris;

    Transform thisTransform;
    void Start()
    {
        for (int x = 0; x < maxDebris; x++)
        {
            
        // Instantiate(debris, Random.insideUnitSphere * distance + thisTransform.position, Random.rotation) as GameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
