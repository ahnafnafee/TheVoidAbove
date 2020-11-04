using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warning : MonoBehaviour
{
    [SerializeField]
    GameObject UI;
    
    private void OnTriggerStay(Collider other)
    {
        UI.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        UI.SetActive(false);
        Debug.Log("Entered");
    }
}
