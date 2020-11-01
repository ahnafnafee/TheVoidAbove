using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDespawn : MonoBehaviour
{
    public float bulletTimer;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        bulletTimer -= Time.deltaTime;

        if(bulletTimer <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if(other.transform.tag == "Citizen")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }

    }
}
