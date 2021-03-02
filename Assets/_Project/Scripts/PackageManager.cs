﻿using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts
{
    public class PackageManager : MonoBehaviour
    {
        public bool hasPackage;
        
        public GameObject package;
        [SerializeField]
        private GameObject grabRange;
        [SerializeField]
        private Transform packagePos;
        [SerializeField]
        private ObjectiveSystem objective;
        private bool startTimer;
        private float pgTimer = 1;
        // Start is called before the first frame update
        void Awake()
        {
            grabRange.SetActive(true);
            package.GetComponent<Rigidbody>().detectCollisions = true;
            package.transform.parent = null;
            startTimer = false;
        }

        void Start()
        {
            if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                hasPackage = false;
            }
            else
            {
                hasPackage = true;
                PickUp();
            }
        }

        // Update is called once per frame
        void Update()
        {
            if(startTimer)
            {
                pgTimer -= Time.deltaTime;
                if(pgTimer <= 0)
                {
                    package.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                    pgTimer = 1;
                    startTimer = false;
                }
            }
        }

        public void Drop(Vector3 vel)
        {
            hasPackage = false;

            // Needs to be modular
            if (SceneManager.GetActiveScene().buildIndex == 3)
                objective.Pending();

            package.transform.parent = null;
            package.GetComponent<Rigidbody>().detectCollisions = true;
            package.GetComponent<Rigidbody>().isKinematic = false;
            package.GetComponent<Rigidbody>().velocity += Vector3.Reflect(vel * 0.5f, vel.normalized);
            startTimer = true;
            grabRange.SetActive(true);
        }

        public void PickUp()
        {
            //TODO: Reference child object directly as hierarchical changes will create bugs
            
            package.transform.position = packagePos.position;
            package.transform.rotation = packagePos.rotation;
            package.transform.parent = this.transform;
            hasPackage = true;

            if (SceneManager.GetActiveScene().buildIndex == 3)
                objective.Complete();

            AkSoundEngine.PostEvent("success_pickup_event", gameObject);
            package.GetComponent<Rigidbody>().detectCollisions = false;
            package.GetComponent<Rigidbody>().isKinematic = true;
            grabRange.SetActive(false);
        }
    }
}
