using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

namespace _Project.Scripts
{
    public class EnemyAim : MonoBehaviour
    {
        private GameObject target;
        [SerializeField] private GameObject enemyModel;
        [SerializeField]
        private float enemySpeed;
        private Quaternion initialRotation;
        private Quaternion modelRotation;


        void Start()
        {
            target = GameObject.FindWithTag("Player");
            initialRotation = transform.parent.rotation;
            modelRotation = enemyModel.transform.rotation;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals(target.tag))
            {
                Quaternion.Lerp(transform.rotation, target.transform.rotation, 0.2f);
                transform.parent.GetComponent<EnemyMove>().StartAiming();
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.tag.Equals(target.tag))
            {
                Quaternion.Lerp(enemyModel.transform.rotation, modelRotation, 0.5f);
                var parent = transform.parent;
                parent.rotation = initialRotation;
                parent.GetComponent<EnemyMove>().StopAiming();
            }
        }

        private void SmoothLookAt(Vector3 newDirection){
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(newDirection), 0.2f);
        }
    }
}
