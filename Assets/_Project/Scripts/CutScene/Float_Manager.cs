using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float_Manager : MonoBehaviour
{
    public float maxCap;
    public float minCap;
    public int HorOrVer;
    public float floatTimer;
    public float myFloatTimer;
    public int UpOrDown;
    public float speed;
    private Vector3 position_original;

    // Start is called before the first frame update
    void Start()
    {
        //if this variable is 1 then move vertically
        floatTimer = myFloatTimer;
        UpOrDown = 1;
        position_original = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(HorOrVer == 1)
        {
            if ((transform.position.y >= maxCap) || (transform.position.y <= minCap))
            {
                floatTimer -= Time.deltaTime;
                if (floatTimer <= 0)
                {
                    if (transform.position.y >= maxCap)
                    {
                        UpOrDown = 0;
                        transform.position += new Vector3(0f, -.005f* speed, 0f);
                    }
                    else
                    {
                        UpOrDown = 1;
                        transform.position += new Vector3(0f, .005f * speed, 0f);
                    }
                }
            }
            else if (transform.position.y < maxCap && UpOrDown == 1)
            {
                //Debug.Log("I'm lifting");
                transform.position += new Vector3(0f, .005f * speed, 0f);
                floatTimer = myFloatTimer;
            }
            else if(transform.position.y > minCap && UpOrDown == 0)
            {
                //Debug.Log("I'm failing");
                transform.position += new Vector3(0f, -.005f * speed, 0f);
                floatTimer = myFloatTimer;
            }
        }
        else
        {
            if ((transform.position.x >= maxCap) || (transform.position.x <= minCap))
            {
                floatTimer -= Time.deltaTime;
                if (floatTimer <= 0)
                {
                    if (transform.position.x >= maxCap)
                    {
                        UpOrDown = 0;
                        transform.position += new Vector3(-.005f * speed, 0f, 0f);
                    }
                    else
                    {
                        UpOrDown = 1;
                        transform.position += new Vector3(.005f * speed, 0f, 0f);
                    }
                }
            }
            else if (transform.position.x < maxCap && UpOrDown == 1)
            {
                transform.position += new Vector3(.005f * speed, 0f, 0f);
                floatTimer = myFloatTimer;
            }
            else if (transform.position.x > minCap && UpOrDown == 0)
            {
                transform.position += new Vector3(-.005f * speed, 0f, 0f);
                floatTimer = myFloatTimer;
            }
        }
    }

	public void Reset()
	{
        speed = 1;
        transform.position = position_original;
	}
}
