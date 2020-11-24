using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts
{
    public class GunPickUp : MonoBehaviour
    {
        PlayerControls _playerControls;
        private Vector3 dropSize;

        [SerializeField]
        private GameObject grabUI;

        private GameObject gun;

        private bool inRange;

        // Start is called before the first frame update
        void Start()
        {

        }

        void Awake()
        {
            inRange = false;
            _playerControls = new PlayerControls();
            _playerControls.Enable();
            dropSize = GameObject.Find("GunMain2").transform.localScale;
            grabUI.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (inRange)
            {
                grabUI.SetActive(true);
            }
            else
            {
                grabUI.SetActive(false);
            }
            PlayerControls.PlayerStandardActions actions = _playerControls.PlayerStandard;
            if (inRange && actions.PickUp.triggered)
            {
                PickUp();
            }
        }

        private void PickUp()
        {
            GameObject dropped = FindEquippedGun();
            dropped.transform.parent = null;
            gun.transform.parent = this.transform;
            gun.transform.position = this.transform.Find("GunPos").position;
            gun.transform.rotation = this.transform.Find("GunPos").rotation;
            gun.transform.localScale = this.transform.Find("GunPos").localScale;
            gun.GetComponent<WeaponScript>().pickUp();
            dropped.GetComponent<WeaponScript>().drop();
            dropped.transform.localScale = dropSize;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.name.Contains("Gun") && other.GetComponent<WeaponScript>().isDropped())
            {
                gun = other.gameObject;
                inRange = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.transform.name.Contains("Gun") && other.GetComponent<WeaponScript>().isDropped())
            {
                gun = null;
                inRange = false;
            }
        }

        private GameObject FindEquippedGun()
        {
            foreach(Transform child in this.transform)
            {
                if(child.name.Contains("GunMain"))
                {
                    return child.gameObject;
                }
            }
            return null;
        }
    }
}
