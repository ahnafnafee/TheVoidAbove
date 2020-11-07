using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts {
    public class BulletScript : MonoBehaviour
    {
        public float lifeDuration = 10f;
        private float lifeTimer;
        public GameObject hitParticlePrefab;

        private void Start()
        {
            lifeTimer = lifeDuration;
        }

        private void Update()
        {
            lifeTimer -= Time.deltaTime;

            if (lifeTimer <= 0f)
            {
                Destroy(gameObject);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            ContactPoint contact = collision.contacts[0];
            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 position = contact.point;
            Instantiate(hitParticlePrefab, position, rotation);
            Destroy(gameObject);
        }
    }
}
