using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyControl : MonoBehaviour
{
    private float timeToChangeDirection;
    // Start is called before the first frame update
    void Start()
    {
        ChangeDirection();
    }

    // Update is called once per frame
    void Update()
    {
        timeToChangeDirection -= Time.deltaTime;

        if (timeToChangeDirection <= 0)
        {
            ChangeDirection();
        }
        GetComponent<Rigidbody>().velocity = transform.up * 10;
    }
    private void ChangeDirection()
    {
        float angle = Random.Range(0f, 360f);
        Quaternion quat = Quaternion.AngleAxis(angle, Vector3.forward);
        Vector3 newUp = quat * Vector3.up;
        newUp.z = Random.Range(-1f, 1f);
        newUp.Normalize();
        transform.up = newUp;
        timeToChangeDirection = 1.5f;
    }
}
