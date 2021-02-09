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
        private float reloadTime; 
        [SerializeField]
        private GameObject bullet;
        [SerializeField]
        private float bulletCount;
        private bool shooting;
        private Vector3 playerPos;
        private float timer, timer2, counter;

        [Header("Weapon Attribute")]
        //Gun stats
        public float spread;
        
        
        void Start()
        {
            shooting = false;
            timer = bulletTimer;
            timer2 = reloadTime;
            counter = bulletCount;
        }

        // Update is called once per frame
        void Update()
        {
            if(shooting)
            {
                if (bulletTimer <= 0)
                {
                    Shoot();
                    counter--;
                    bulletTimer = timer;
                }
                bulletTimer -= Time.deltaTime;
            }
            if (counter <= 0)
            {
                Reload();
            }
        }

        private void Reload()
        {
            shooting = false;
            reloadTime -= Time.deltaTime;
            if(reloadTime <= 0)
            {
                reloadTime = timer2;
                shooting = true;
                counter = bulletCount;
            }
        }
        private void Shoot()
        {
            //Calculate spread
            float x = Random.Range(-spread, spread);
            float y = Random.Range(-spread, spread);
            
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            //grab variables for kinematics
            playerPos = player.transform.position;
            Vector3 playerVelocity = player.GetComponent<Rigidbody>().velocity;
            float t = (playerPos - transform.position).magnitude / bulletSpeed;

            // Predict future position
            Vector3 futurePos = playerPos + playerVelocity * t;

            //Get the overall predicted direction before randomized spread
            Vector3 directionWithoutSpread = (futurePos - transform.position);
            
            
            //Calculate new direction with spread
            Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);
                    
            //Fire
            GameObject newBullet = Instantiate(bullet, transform.position, bullet.transform.rotation);
            newBullet.GetComponent<Rigidbody>().velocity = directionWithSpread.normalized * bulletSpeed;
            AkSoundEngine.PostEvent("robot_shoot_event", this.gameObject);
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
