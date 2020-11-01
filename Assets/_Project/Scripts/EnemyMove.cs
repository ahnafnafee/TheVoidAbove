using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    private float enemySpeed;
    private bool right; 
    // Start is called before the first frame update
    void Start()
    {
        right = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(right)
        {
            transform.Translate(0,0, 2 * Time.deltaTime * enemySpeed);
        }
        else
        {
            transform.Translate(0, 0, -2 * Time.deltaTime * enemySpeed);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        print("Hello");
        if (other.transform.tag.Equals("Turn"))
        {
            right = !right;
        }
    }
}
