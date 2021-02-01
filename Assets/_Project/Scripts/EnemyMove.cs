using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts
{
    public class EnemyMove : MonoBehaviour
    {
        [SerializeField]
        private float enemySpeed;
        private GameObject target;
        private bool right;
        private bool isAiming;
        private Health enemyHealth;
        [Tooltip("Image component displaying current health")]
        public Image healthFillImage;

        public GameObject enemyHealthObj;

        public GameObject hitParticlePrefab;
        [SerializeField] private GameObject gun;

        public Vector3 origianlPosition;
        public bool returnMove;

        // Start is called before the first frame update
        void Start()
        {
            target = GameObject.FindWithTag("Player");
            isAiming = false;
            right = false;
            returnMove = false;
            enemyHealth = GetComponent<Health>();
            transform.Find("Anvil Enemy Gun up").rotation = Quaternion.Euler(0, 180, 0);
            origianlPosition = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            aimCheck();
            if (isAiming == false)
            {
                if (returnMove)
                {
                    if(Vector3.Distance(origianlPosition, transform.position) >= 1f)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, origianlPosition, 2*  enemySpeed * Time.deltaTime);
                        Vector3 direction = origianlPosition - transform.position;
                        transform.Find("Anvil Enemy Gun up").rotation = Quaternion.Slerp(transform.Find("Anvil Enemy Gun up").rotation, Quaternion.LookRotation(direction), 2 * Time.deltaTime * enemySpeed);
                    }
                    else
                    {
                        returnMove = false;
                    }
                }
                else
                {
                    if (right == true)
                    {
                        transform.position += new Vector3(0, 0, 2 * enemySpeed * Time.deltaTime);
                        Vector3 direction = (transform.position + new Vector3(0, 0, 2 * enemySpeed * Time.deltaTime)) - transform.position;
                        transform.Find("Anvil Enemy Gun up").rotation = Quaternion.Slerp(transform.Find("Anvil Enemy Gun up").rotation, Quaternion.LookRotation(direction), 2 * Time.deltaTime * enemySpeed);
                    }
                    else if (right == false)
                    {
                        transform.position += new Vector3(0, 0, -2 * enemySpeed * Time.deltaTime);
                        Vector3 direction = (transform.position + new Vector3(0, 0, -2 * enemySpeed * Time.deltaTime)) - transform.position;
                        transform.Find("Anvil Enemy Gun up").rotation = Quaternion.Slerp(transform.Find("Anvil Enemy Gun up").rotation, Quaternion.LookRotation(direction), 2 * Time.deltaTime * enemySpeed);

                    }
                }
            }
            else if(isAiming)
            {
                Vector3 direction = target.transform.position - transform.position;
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, enemySpeed * Time.deltaTime);
                transform.Find("Anvil Enemy Gun up").rotation = Quaternion.Slerp(transform.Find("Anvil Enemy Gun up").rotation, Quaternion.LookRotation(direction), 2 * Time.deltaTime * enemySpeed);
                //transform.Find("Anvil Enemy Gun up").LookAt(target.transform);
            }
            
            enemyHealthObj.transform.LookAt(target.transform);
            healthFillImage.fillAmount = enemyHealth.objectHealth / enemyHealth.health;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name == "wallLeft")
            {
                right = true;
            }
            if (other.gameObject.name == "wallRight")
            {
                right = false;
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

        public void StartAiming()
        {
            isAiming = true;
            gun.GetComponent<EnemyGun>().startShooting();
        }
        public void StopAiming()
        {
            isAiming = false;
            gun.GetComponent<EnemyGun>().stopShooting();
            returnMove = true;
        }
        public void aimCheck()
        {
            Debug.Log((Vector3.Distance(target.transform.position, transform.position)));
            if ((Vector3.Distance(target.transform.position, transform.position)) <= 300f)
            {
                StartAiming();
            }
            else
            {
                if (isAiming)
                {
                    StopAiming();
                }
            }
        }
    }
}
