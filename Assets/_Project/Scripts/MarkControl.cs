using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkControl : MonoBehaviour
{
    private float time;
    private float time_destroy;
    // Start is called before the first frame update
    void Start()
    {
        time_destroy = 1.5f;
        time = time_destroy;
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        else {
            Destroy(this.gameObject);
        }
    }
}
