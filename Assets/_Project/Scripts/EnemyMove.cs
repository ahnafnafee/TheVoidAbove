using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts
{
    public class EnemyMove : MonoBehaviour
    {
        [SerializeField]
        private float enemySpeed;
        [SerializeField]
        private GameObject target;
        private bool right;
        private bool isAiming = false;
        private Health enemyHealth;
        [Tooltip("Image component displaying current health")]
        public Image healthFillImage;

        public GameObject enemyHealthObj;

        public GameObject hitParticlePrefab;
        // Start is called before the first frame update
        void Start()
        {
            right = false;
            enemyHealth = GetComponent<Health>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!isAiming)
            {
                if (right)
                {
                    transform.position += new Vector3(0, 0, 2 * enemySpeed * Time.deltaTime);
                }
                else
                {
                    transform.position += new Vector3(0, 0, -2 * enemySpeed * Time.deltaTime);
                }
            }
            else if(isAiming)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, enemySpeed * Time.deltaTime);
            }
            
            enemyHealthObj.transform.LookAt(target.transform);
            healthFillImage.fillAmount = enemyHealth.objectHealth / enemyHealth.health;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.tag.Equals("Turn"))
            {
                if (other.transform.tag.Equals("Turn"))
                {
                    right = !right;
                }
            }
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.CompareTag("Bullet"))
            {
                ContactPoint contact = collision.contacts[0];
                Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
                Vector3 position = contact.point;
                Instantiate(hitParticlePrefab, position, rotation);
                
                enemyHealth.TakeDamage(1);
            }
        }

        public void startAiming()
        {
            isAiming = true;
            this.transform.GetChild(1).GetComponent<EnemyGun>().startShooting();
        }
        public void stopAiming()
        {
            this.transform.GetChild(1).GetComponent<EnemyGun>().stopShooting();
            isAiming = false;
        }
    }
}
