using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

namespace _Project.Scripts
{
    public class EnemyAim : MonoBehaviour
    {
        private GameObject target;
        [SerializeField]
        private float enemySpeed;
        private Quaternion initialRotation;
        // Start is called before the first frame update
        void Start()
        {
            target = GameObject.FindWithTag("Player");
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
                this.transform.parent.parent.GetComponent<EnemyMove>().StartAiming();
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.tag.Equals(target.tag))
            {
                this.transform.parent.rotation = initialRotation;
                this.transform.parent.GetComponent<EnemyMove>().StopAiming();
            }
        }
    }
}
