using UnityEngine;

namespace _Project.Scripts
{
    public class BulletManager : MonoBehaviour
    {
        public float bulletTimer;
        public GameObject hitParticlePrefab;
        // Start is called before the first frame update
        void Start()
        {
        }

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
                collision.transform.GetComponent<Health>().TakeDamage(10);
                Destroy(gameObject);

            }
            else if(collision.transform.CompareTag("Enemy"))
            {
                Destroy(collision.transform);
            }
            
            ContactPoint contact = collision.contacts[0];
            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 position = contact.point;
            Instantiate(hitParticlePrefab, position, rotation);
            Destroy(gameObject);
        }
    }
}