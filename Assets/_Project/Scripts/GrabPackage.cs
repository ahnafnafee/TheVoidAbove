using _Project.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabPackage : MonoBehaviour
{
    PlayerControls _playerControls;
    private bool inRange;
    // Start is called before the first frame update
    void Awake()
    {
        inRange = false;
        _playerControls = new PlayerControls();
        _playerControls.Enable();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerControls.PlayerStandardActions actions = _playerControls.PlayerStandard;
        if (inRange && actions.PickUp.triggered)
        {
            GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PackageManager>().pickUp();
        }
    }

    private void OnTriggerEnter(Collider other)
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
