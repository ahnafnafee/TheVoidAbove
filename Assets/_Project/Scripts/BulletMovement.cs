using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts {
    public class BulletMovement : MonoBehaviour
    {
        public float speed;
        public GameObject hitParticlePrefab;
        Rigidbody rb;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            transform.position += transform.forward * (speed * Time.deltaTime);
        }

        private void OnCollisionEnter(Collision other)
        {
            Debug.Log($"Object name: {other.gameObject.name}");
            if (other.gameObject.CompareTag("Enemy"))
            {
                Instantiate(hitParticlePrefab, transform.position, transform.rotation);
                // Destroy(other.gameObject);
                
            }
        }
    }
}
