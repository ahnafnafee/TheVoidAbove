using UnityEngine;

namespace _Project.Scripts
{
    public class EnemyGun : MonoBehaviour
    {
        [SerializeField]
        private float bulletTimer;
        [SerializeField]
        private float bulletSpeed;
        [SerializeField]
        private GameObject bullet;
        private bool shooting;
        private Vector3 playerPos;
        private float timer;

        [Header("Weapon Attribute")]
        //Gun stats
        public float spread;
        
        
        void Start()
        {
            shooting = false;
            timer = bulletTimer;
        }

        // Update is called once per frame
        void Update()
        {
            if(shooting)
            {
                if (bulletTimer <= 0)
                {
                    Shoot();
                    bulletTimer = timer;
                }
                bulletTimer -= Time.deltaTime;
            }
        }

        private void Shoot()
        {
            //Calculate spread
            float x = Random.Range(-spread, spread);
            float y = Random.Range(-spread, spread);
            
            playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;

            Vector3 directionWithoutSpread = playerPos - transform.position;
            //Calculate new direction with spread
            Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);
                    
            GameObject newBullet = Instantiate(bullet, transform.position, bullet.transform.rotation);
            newBullet.GetComponent<Rigidbody>().velocity = directionWithSpread.normalized * bulletSpeed;        
        }
        
        public void startShooting()
        {
            shooting = true;
        }
        public void stopShooting()
        {
            shooting = false;
        }
    }
}
