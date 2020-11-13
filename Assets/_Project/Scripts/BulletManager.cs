using UnityEngine;

namespace _Project.Scripts
{
    public class BulletManager : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        // TODO: BulletManager, BulletDespawn and BulletScript needs to be combined
        private void OnCollisionEnter(Collision collision)
        {
            if(collision.transform.CompareTag("Player"))
            {
                collision.transform.GetComponent<Health>().TakeDamage(10);
                Destroy(gameObject);

            }
            else if(collision.transform.CompareTag("Enemy"))
            {
                Destroy(collision.transform);
            }
        }
    }
}
