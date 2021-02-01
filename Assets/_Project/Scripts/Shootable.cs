using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts
{
    public class Shootable : MonoBehaviour
    {
        [SerializeField]
        private double health;
        
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
            if (collision.transform.CompareTag("Bullet"))
            {
                if (health > 0)
                {
                    health -= 1;
                }
                else
                {
                    Destroy(this.transform.gameObject);
                }
            }
        }
    }
}

