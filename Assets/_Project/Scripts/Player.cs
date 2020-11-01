using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float thrusteraccel;
    [SerializeField]
    //Currently unimplemented
    private float thrustermaxspeed;
    [SerializeField]
    private float gunmovespeed,rotSpeed,maxSpeed;
    [SerializeField]
    private int playerHealth;

    private Rigidbody rb;
    PlayerControls playercontrols;
    
    [Header("Camera Controls")] 
    [SerializeField] private Camera mainCam;
    [SerializeField] private float lSpeed = 4f;

    void Awake()
    {
        playercontrols = new PlayerControls();
        playercontrols.Enable();

        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
        
    }
    
    // Update is called once per frame
    void Update()
    {
        // transform.rotation = Quaternion.Lerp(transform.rotation, mainCam.transform.rotation, lSpeed);
    }
    

    void FixedUpdate()
    {
        PlayerControls.PlayerStandardActions Actions = playercontrols.PlayerStandard;

        //Apply forces based on the wasd/spc/shift controls in character controller
        rb.AddForce(thrusteraccel * (Actions.ThrustersY.ReadValue<float>()* Camera.main.transform.up +  Actions.ThrustersX.ReadValue<float>() * Camera.main.transform.right + (Actions.ThrustersZ.ReadValue<float>() * Camera.main.transform.forward)).normalized, ForceMode.Acceleration);

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
        
        //When you click LMB, fire the gun (nothing happens like shooting a projectile) and fire the player backwards
        if (Actions.Gun.triggered)
            rb.AddForce(-Camera.main.transform.forward * gunmovespeed, ForceMode.VelocityChange);

    }

    public void takeDamage(int damage)
    {
        if ((playerHealth - damage) > 0)
        {
            playerHealth -= damage;
        }
        else
        {
            print("You lose");
        }
        
    }
}
