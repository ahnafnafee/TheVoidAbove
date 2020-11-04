﻿using UnityEngine;

namespace _Project.Scripts
{
    public class PackageManager : MonoBehaviour
    {
        bool hasPackage;
        public GameObject package;
        [SerializeField]
        private GameObject grabRange;
        // Start is called before the first frame update
        void Awake()
        {
            hasPackage = true;
            grabRange.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void drop()
        {
            hasPackage = false;
            package.transform.parent = null;
            grabRange.SetActive(true);
        }

        public void pickUp()
        {
            //TODO: Reference child object directly as hierarchical changes will create bugs
            
            package.transform.position = this.transform.GetChild(this.transform.childCount - 1).position;
            package.transform.rotation = this.transform.GetChild(this.transform.childCount - 1).rotation;
            package.transform.parent = this.transform;
            hasPackage = true;
            grabRange.SetActive(false);
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.tag == "Debris" && hasPackage)
            {
                drop();
            }
        }
    }
}
