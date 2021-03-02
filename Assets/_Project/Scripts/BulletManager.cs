using UnityEngine;

namespace _Project.Scripts
{
    public class BulletManager : MonoBehaviour
    {
        public float lifeDuration = 10f;
        public float bulletTimer;
        public GameObject hitParticlePrefab;
        public Transform enemyLocation;
        [SerializeField] private bool isPlayer = false;

        private void Start()
        {
            bulletTimer = lifeDuration;
        }

        // Update is called once per frame
        private void Update()
        {
            bulletTimer -= Time.deltaTime;

            if(bulletTimer <= 0f)
            {
                Destroy(gameObject);
            }
        }
        
        
        private void OnCollisionEnter (Collision collision)
        {

            if (isPlayer && collision.gameObject.CompareTag("Player"))
            {
                Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), gameObject.GetComponent<Collider>());
                return;
            }

            if (collision.transform.CompareTag("Player"))
            {
                collision.transform.GetComponent<Health>().TakeDamage(6);
                DI_System.CreateIndicator(enemyLocation);
                Destroy(gameObject);
            }

            ContactPoint contact = collision.contacts[0];
            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 position = contact.point;
            Instantiate(hitParticlePrefab, position, rotation);
            Destroy(gameObject);
        }
    }
}