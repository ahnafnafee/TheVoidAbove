using UnityEngine;
using System;


public class Twist:MonoBehaviour
{
    public float twist = 10.0f;
    
    
    public void Update() {
    transform.Rotate(Vector3.up*twist*Time.deltaTime);
    }
}