using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

namespace _Project.Scripts
{
    public class EnemyAim : MonoBehaviour
    {
        [SerializeField]
        private GameObject target;
        [SerializeField]
        private float enemySpeed;
        private Quaternion initialRotation;
        // Start is called before the first frame update
        void Start()
        {
            initialRotation = this.transform.parent.rotation;
        }

        // Update is called once per frame
        void Update()
        {
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals(target.tag))
            {
                this.transform.parent.GetComponent<EnemyMove>().startAiming();
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.tag.Equals(target.tag))
            {
                this.transform.parent.rotation = initialRotation;
                this.transform.parent.GetComponent<EnemyMove>().stopAiming();
            }
        }
    }
}
