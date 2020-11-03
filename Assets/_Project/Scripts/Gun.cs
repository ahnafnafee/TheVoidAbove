using System;
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
        // PlayerControls.PlayerStandardActions actions = _playerControls.PlayerStandard;
        //
        // if (actions.Gun.triggered)
        //     Shoot();
    }
    
    void Shoot()
    {
        Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0f); // center of the screen
        float rayLength = 500f;
        
        Ray ray = Camera.main.ViewportPointToRay(rayOrigin);
        
        Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayLength))
        {
            Debug.Log(hit.transform.name);
        }
        
        // RaycastHit hit;
        // if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit, range))
        // {
        //     Debug.Log(hit.transform.name);
        //     Debug.DrawRay(mainCam.transform.position, mainCam.transform.forward, Color.yellow);
        // }
        // _rb.AddForce(-mainCam.transform.forward * gunmovespeed, ForceMode.VelocityChange);
    }
}
