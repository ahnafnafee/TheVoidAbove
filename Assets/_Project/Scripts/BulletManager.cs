using UnityEngine;

namespace _Project.Scripts
{
    public class BulletManager : MonoBehaviour
    {
        public float bulletTimer;
        public GameObject hitParticlePrefab;
        public Transform enemyLocation;

        // Update is called once per frame
        void Update()
        {
            bulletTimer -= Time.deltaTime;

            if(bulletTimer <= 0)
            {
                Destroy(gameObject);
            }
        }
        
        
        private void OnCollisionEnter (Collision collision)
        {
            if (collision.transform.CompareTag("Player"))
            {
                collision.transform.GetComponent<Health>().TakeDamage(6);
                DI_System.CreateIndicator(enemyLocation);
                Destroy(gameObject);
            }
            Debug.Log("hit something");
            ContactPoint contact = collision.contacts[0];
            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 position = contact.point;
            Instantiate(hitParticlePrefab, position, rotation);
            Destroy(gameObject);
        }
    }
}