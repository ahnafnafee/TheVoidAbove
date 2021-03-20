using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] Transform respawnPoint, player;
    



    private void OnTriggerEnter(Collider other)
    {
       
       player.transform.position = respawnPoint.transform.position;
       
    }


}
