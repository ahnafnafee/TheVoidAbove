using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageManager : MonoBehaviour
{
    bool hasPackage;
    public GameObject package;
    // Start is called before the first frame update
    void Start()
    {
        hasPackage = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void drop()
    {
        hasPackage = false;
        package.transform.parent = null;
    }

    public void pickUp()
    {
        package.transform.parent = this.transform;
        hasPackage = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Debris" && hasPackage)
        {
            drop();
        }
        if (collision.transform.tag == "Package" && !hasPackage)
        {
            pickUp();
        }
    }
}
