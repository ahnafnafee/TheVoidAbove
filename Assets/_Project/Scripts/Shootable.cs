using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts
{
    public class Shootable : MonoBehaviour
    {
        [SerializeField]
        private double health;

        [SerializeField]
        private GameObject partner;
        [SerializeField] 
        private GameObject drop;
        [SerializeField]
        private GameObject healthDrop;
        [SerializeField]
        private GameObject explosionFx;
        [SerializeField]
        private bool droppableObject;

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
                    if (droppableObject) 
                    { 
                        var transform1 = this.transform;
                        Instantiate(explosionFx, transform1.position, transform1.rotation);

                        if (Random.Range(0, 10) <= 4)
                        {
                            Instantiate(drop, transform1.position, Quaternion.identity);
                        }
                        else
                        {
                            Instantiate(healthDrop, transform1.position, Quaternion.identity);
                        }
                    }
                    Destroy(gameObject);
                    if (partner != null)
                        Destroy(partner.transform.gameObject);
                }
            }
        }
    }
}

