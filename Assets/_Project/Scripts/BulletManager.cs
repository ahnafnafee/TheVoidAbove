using UnityEngine;

namespace _Project.Scripts
{
    public class BulletManager : MonoBehaviour
    {
        public int damage = 6;
        public Vector3 start_pos;
        public float lifeDuration;
        public float bulletTimer;
        public GameObject hitParticlePrefab;
        public Transform enemyLocation;
        [SerializeField] private bool isPlayer = false;

        private void Start()
        {
            start_pos = transform.position;
            bulletTimer = lifeDuration;
        }

        // Update is called once per frame
        private void Update()
        {
            bulletTimer -= Time.deltaTime;

            if(Vector3.Distance(transform.position, start_pos)>=300f || bulletTimer <= 0f)
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

            if (!isPlayer && collision.gameObject.CompareTag("Enemy"))
            {
                Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), gameObject.GetComponent<Collider>());
                return;
            }
            if (collision.transform.CompareTag("Player"))
            {
                collision.transform.GetComponent<Health>().TakeDamage(damage);
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