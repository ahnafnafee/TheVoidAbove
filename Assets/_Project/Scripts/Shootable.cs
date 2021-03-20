using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts
{
    public class Shootable : MonoBehaviour
    {
        [SerializeField]
        private double health;

        [SerializeField]
        private GameObject partner;
        [FormerlySerializedAs("drop")] [SerializeField]
        private GameObject ammoDrop;
        [SerializeField]
        private GameObject healthDrop;
        [SerializeField]
        private GameObject explosionFx;
        [SerializeField]
        private bool droppableObject;

        private List<GameObject> drops;
        private readonly System.Random random = new System.Random();


        void Start()
        {
            drops = new List<GameObject>(new[]{ammoDrop, healthDrop});
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.transform.CompareTag("Bullet")) return;

            if (health > 0)
            {
                health -= 1;
            }
            else
            {
                var transform1 = transform;

                if (explosionFx != null)
                    Instantiate(explosionFx, transform1.position, transform1.rotation);

                if (droppableObject)
                {
                    var powerUp = drops[random.Next(drops.Count)];
                    Instantiate(powerUp, transform1.position, Quaternion.identity);
                }

                Destroy(gameObject);

                if (partner != null)
                    Destroy(partner.transform.gameObject);
            }
        }
    }
}

