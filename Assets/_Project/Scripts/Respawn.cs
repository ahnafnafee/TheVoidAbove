using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] Transform respawnPoint,player;

    private void OnCollisionEnter(Collision collision)
    {
        player.transform.position = respawnPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
