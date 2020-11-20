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
    void Update()
    {
        if (inRange)
        {
            grabUI.SetActive(true);
        }
        else
        {
            grabUI.SetActive(false);
        }
        PlayerControls.PlayerStandardActions actions = _playerControls.PlayerStandard;

        if (actions.PickUp.triggered)
        {
            Debug.Log("E Pressed");
        }

        if (inRange && actions.PickUp.triggered)
        {
            Debug.Log("Picked Up");
            GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PackageManager>().pickUp();
            grabUI.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            inRange = true;
            Debug.Log("Enter");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            inRange = false;
            Debug.Log("Exit");
        }
    }
}
