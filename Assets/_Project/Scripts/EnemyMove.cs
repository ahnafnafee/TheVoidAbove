using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts
{
    public class EnemyMove : MonoBehaviour
    {
        private GameObject target;
        private bool right;
        private bool isAiming;

        private Health enemyHealth;

        private Transform enemyBody;

        [Header("Enemy Attributes")]
        [SerializeField] private float enemySpeed;
        [SerializeField] private float enemyRange = 300f;
        [SerializeField] private GameObject enemyObj;
        public GameObject mark_aggro;

        [Header("Enemy Health")]
        public Image healthFillImage;
        public GameObject enemyHealthObj;

        [Header("Misc")]
        [SerializeField] private GameObject hitParticlePrefab;
        [SerializeField] private GameObject gun;
        [SerializeField] private Vector3 origianlPosition;
        [SerializeField] private bool returnMove;

        // Start is called before the first frame update
        void Start()
        {
            target = GameObject.FindWithTag("Player");
            isAiming = false;
            right = false;
            returnMove = false;
            enemyHealth = GetComponent<Health>();
            enemyObj.transform.rotation = Quaternion.Euler(0, 180, 0);
            origianlPosition = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            AimCheck();

            enemyBody = enemyObj.transform;

            if (isAiming == false)
            {
                if (returnMove)
                {
                    if (Vector3.Distance(origianlPosition, transform.position) >= 1f)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, origianlPosition, 2*  enemySpeed * Time.deltaTime);
                        //transform.position = Vector3.MoveTowards(transform.position, origianlPosition, 2*  enemySpeed * Time.deltaTime);
                        Vector3 direction = origianlPosition - transform.position;
                        if (direction != Vector3.zero)
                            enemyBody.rotation = Quaternion.Slerp(enemyBody.rotation, Quaternion.LookRotation(direction), 2 * Time.deltaTime * enemySpeed);
                    }
                    else
                    {
                        returnMove = false;
                    }
                }
                else
                {
                    if (right)
                    {
                        transform.position += new Vector3(0, 0, 2 * enemySpeed * Time.deltaTime);
                        Vector3 direction = (transform.position + new Vector3(0, 0, 2 * enemySpeed * Time.deltaTime)) - transform.position;
                        if (direction != Vector3.zero)
                            enemyBody.rotation = Quaternion.Slerp(enemyBody.rotation, Quaternion.LookRotation(direction), 2 * Time.deltaTime * enemySpeed);
                    }
                    else
                    {
                        transform.position += new Vector3(0, 0, -2 * enemySpeed * Time.deltaTime);
                        Vector3 direction = (transform.position + new Vector3(0, 0, -2 * enemySpeed * Time.deltaTime)) - transform.position;
                        if (direction != Vector3.zero)
                            enemyBody.rotation = Quaternion.Slerp(enemyBody.rotation, Quaternion.LookRotation(direction), 2 * Time.deltaTime * enemySpeed);

                    }
                }
            }
            else if (isAiming)
            {
                Vector3 direction = target.transform.position - transform.position;
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, enemySpeed * Time.deltaTime);
                if (direction != Vector3.zero)
                    enemyBody.rotation = Quaternion.Slerp(enemyBody.rotation, Quaternion.LookRotation(direction), 2 * Time.deltaTime * enemySpeed);
                //enemyBody.LookAt(target.transform);
            }
            
            enemyHealthObj.transform.LookAt(target.transform);
            healthFillImage.fillAmount = enemyHealth.objectHealth / enemyHealth.health;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals("Turn"))
            {
                right = !right;
            }
            /*if (other.gameObject.name == "wallLeft")
            {
                right = true;
            }
            if (other.gameObject.name == "wallRight")
            {
                right = false;
            }*/
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.CompareTag("Bullet"))
            {
                ContactPoint contact = collision.contacts[0];
                Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
                Vector3 position = contact.point;
                Instantiate(hitParticlePrefab, position, rotation);

                if (collision.gameObject.name == "Bullet 3(Clone)")
                {
                    if (GameObject.Find("Player").GetComponent<Player>().isPowered())
                    {
                        enemyHealth.TakeDamage(4);
                    }
                    else { 
                        enemyHealth.TakeDamage(1); 
                    }
                    StartAiming();
                }
            }
        }

        public void StartAiming()
        {
            if (!isAiming)
            {
                isAiming = true;
                CreateMark();
                gun.GetComponent<EnemyGun>().startShooting();
            }
        }
        public void StopAiming()
        {
            isAiming = false;
            gun.GetComponent<EnemyGun>().stopShooting();
            returnMove = true;
        }

        // FYI this method might be very expensive
        public void AimCheck()
        {

            if ((Vector3.Distance(target.transform.position, transform.position)) <= enemyRange)
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
        private void CreateMark()
        {
            Vector3 pos_new = transform.position + new Vector3(0f, 50f, 0f);
            GameObject mark = Instantiate(mark_aggro, pos_new, transform.rotation);
            mark.transform.SetParent(transform);

        }
    }
}
