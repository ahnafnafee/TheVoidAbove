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
        private float timer;
        // Start is called before the first frame update
        void Start()
        {
            timer = bulletTimer;
        }

        // Update is called once per frame
        void Update()
        {
            if(bulletTimer <= 0)
            {
                GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
                newBullet.GetComponent<Rigidbody>().velocity = transform.up * bulletSpeed;
                bulletTimer = timer;
            }
            bulletTimer -= Time.deltaTime;
        }
    }
}
