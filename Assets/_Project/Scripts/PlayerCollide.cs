using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollide : MonoBehaviour
{
    
    private Rigidbody rb;
    Vector3 normal;
    private ContactPoint contact;

    // Start is called before the first frame update
    void Awake()
    {
        
        rb = GetComponent<Rigidbody>();

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision col)
    {

        //Add anything you want to happen when the player collides in here. It updates akin to Update
        //TODO:Produce some conditionals and tag our assets to discriminate in collisions (I.E.: when they hit a big rock vs a tiny rock, the tiny debris should yield instead of the player)

        foreach (ContactPoint contact in col.contacts);
        { 
            normal += contact.normal;
        }

        normal.Normalize();

        rb.velocity = Vector3.Reflect(rb.velocity, normal);
    }
}
