using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiggerControl : MonoBehaviour
{
    [SerializeField] private GameObject dialogueObject;
    private float control = 0;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(control == 0)
            {
                dialogueObject.SetActive(true);
            }
            control += 1;
        }
    }
}
