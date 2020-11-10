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
        // Start is called before the first frame update
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
                    playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
                    GameObject newBullet = Instantiate(bullet, transform.position, bullet.transform.rotation);
                    newBullet.GetComponent<Rigidbody>().velocity = (playerPos - transform.position).normalized * bulletSpeed;        
                    bulletTimer = timer;
                }
                bulletTimer -= Time.deltaTime;
            }
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
