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

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.transform.tag.Equals("Player"))
            {
                collision.transform.GetComponent<Player>().TakeDamage(1);
            }
            else if(collision.transform.tag.Equals("Enemy"))
            {
                Destroy(collision.transform);
            }
        }
    }
}
