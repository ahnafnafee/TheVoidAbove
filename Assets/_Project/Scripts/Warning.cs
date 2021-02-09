using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warning : MonoBehaviour
{
    [SerializeField]
    GameObject UI;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("OuterZone"))
        {
            UI.SetActive(false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("OuterZone")) {
            UI.SetActive(true);
        }

    }
}
