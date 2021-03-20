using _Project.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{

    public GameObject player;
    public GameObject targetObject;
    public bool package = true;
    public Transform target;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        package = player.GetComponent<PackageManager>().hasPackage;
        checkTarget();
        transform.forward = (target.position - player.transform.position).normalized;   
    }

    public void checkTarget()
    {
        if (package == false)
        {
            targetObject = GameObject.FindGameObjectWithTag("Package");
            target = targetObject.transform;
        }
        else
        {
            targetObject = GameObject.FindGameObjectWithTag("Goal");
            target = targetObject.transform;
        }
    }
}
