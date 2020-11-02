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
        private bool isAiming;
        private Quaternion initialRotation;
        // Start is called before the first frame update
        void Start()
        {
            isAiming = false;
            initialRotation = this.transform.parent.rotation;
        }

        // Update is called once per frame
        void Update()
        {
            if (isAiming)
            {
                this.transform.parent.LookAt(target.transform);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals(target.tag))
            {
                isAiming = true;
                this.transform.parent.GetComponent<EnemyMove>().stopMoving();
            }
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.tag.Equals(target.tag))
            {
                isAiming = true;
                this.transform.parent.GetComponent<EnemyMove>().stopMoving();
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.tag.Equals(target.tag))
            {
                this.transform.parent.rotation = initialRotation;
                isAiming = false;
                this.transform.parent.GetComponent<EnemyMove>().startMoving();
            }
        }
    }
}
