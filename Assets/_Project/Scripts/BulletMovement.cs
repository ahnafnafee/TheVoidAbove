using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts {
    public class BulletMovement : MonoBehaviour
    {
        public float speed, distance = 1000f;
        public GameObject hitParticlePrefab;
        Rigidbody rb;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            Shoot();
        }
        
        private void Shoot()
        {
            // Create a ray from the camera going through the middle of your screen
            Camera mainCam = Camera.main;
            
            Ray ray = mainCam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
            RaycastHit hit ;
            Vector3 targetPoint ;
            
            if (Physics.Raycast(ray, out hit))
                targetPoint = hit.point;
            else
                targetPoint = ray.GetPoint(distance);
            
            gameObject.GetComponent<Rigidbody>().velocity =
                (targetPoint - gameObject.transform.position).normalized * speed;
        }

        private void OnCollisionEnter(Collision other)
        {
            Debug.Log($"Object name: {other.gameObject.name}");
        }
    }
}
