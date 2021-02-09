using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_Manager : MonoBehaviour
{
    private float timer;
    public GameObject myImage;
    private float zAngle = 0;
    // Start is called before the first frame update
    void Start()
    {
        myImage.SetActive(false);
        timer = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 1)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            myImage.SetActive(true);
            if (zAngle > -90f)
            {
                //Debug.Log("I'm lifting");
                zAngle -= .1f;
                transform.eulerAngles = new Vector3(0f, 0f, zAngle);
            }
        }
    }
}
