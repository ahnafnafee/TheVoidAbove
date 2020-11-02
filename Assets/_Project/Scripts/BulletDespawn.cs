using UnityEngine;

namespace _Project.Scripts
{
    public class BulletDespawn : MonoBehaviour
    {
        public float bulletTimer;
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
        void OnCollisionEnter(Collision other)
        {
            if (other.transform.CompareTag("Player"))
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
            }

        }
    }
}
