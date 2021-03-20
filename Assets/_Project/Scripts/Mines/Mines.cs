using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Mines : MonoBehaviour
{
    GameObject player;

    [SerializeField] float speed, rotSpeed;
    [SerializeField] GameObject explosion;

    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void FixedUpdate()
    {

        float dist = Vector3.Distance(player.transform.position, transform.position);

        if (dist < 100)
        {


            Vector3 direction = player.transform.position - rb.position;

            direction.Normalize();


            var up = transform.up;
            Vector3 rotateAmount = Vector3.Cross(direction, up);
            rb.angularVelocity = -rotateAmount * rotSpeed;
            rb.velocity = up * speed;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.CompareTag("Bullet"))
        {
            var transform1 = transform;
            GameObject fx = Instantiate(explosion, transform1.position, transform1.rotation);
            Destroy(fx, 5.0f);
            Destroy(this.gameObject);
        }
        if (col.collider.CompareTag("Player"))
        {
            var transform1 = transform;
            GameObject fx = Instantiate(explosion, transform1.position, transform1.rotation);
            Destroy(fx, 5.0f);
        }
    }
}
