using _Project.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabPackage : MonoBehaviour
{
    [SerializeField] 
    private GameObject grabUI;
    PlayerControls _playerControls;
    private bool inRange;
    // Start is called before the first frame update
    void Awake()
    {
        inRange = false;
        _playerControls = new PlayerControls();
        _playerControls.Enable();
        grabUI.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(inRange)
        {
            grabUI.SetActive(true);
        }
        else
        {
            grabUI.SetActive(false);
        }
        PlayerControls.PlayerStandardActions actions = _playerControls.PlayerStandard;
        if (inRange && actions.PickUp.triggered)
        {
            GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PackageManager>().pickUp();
            grabUI.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag.Equals("Player"))
            inRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
            inRange = false;
    }
}
