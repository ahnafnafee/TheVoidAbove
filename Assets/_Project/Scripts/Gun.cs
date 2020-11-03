using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    PlayerControls _playerControls;
    [SerializeField] private float range = 100f;

    void Awake()
    {
        _playerControls = new PlayerControls();
        _playerControls.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerControls.PlayerStandardActions actions = _playerControls.PlayerStandard;
        
        if (actions.Gun.triggered)
            Shoot();
    }
    
    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
        }
        // _rb.AddForce(-mainCam.transform.forward * gunmovespeed, ForceMode.VelocityChange);
    }
}
