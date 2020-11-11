using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts
{
    public class WeaponScript : MonoBehaviour
    {
        PlayerControls _playerControls;
        public bool allowButtonHold;

        public bool allowInvoke = true;
        public Transform attackPoint;
        [SerializeField] private GameObject bullet;

        int bulletsLeft, bulletsShot;
        
        [Header("Camera")]
        public float camIntensity, camTimer;
        public Camera fpsCam;

        [Header("Bullet Attribute")]
        public int magazineSize, bulletsPerTap;
        public float shootForce, upwardForce;
        [SerializeField] private float bulletTimer;
        private float timer;
        
        //bools
        bool shooting, readyToShoot, reloading;

        [Header("Weapon Attribute")]
        //Gun stats
        public float timeBetweenShooting, spread, reloadTime, timeBetweenShots;

        private void Awake()
        {
            _playerControls = new PlayerControls();
            _playerControls.Enable();
            
            //make sure magazine is full
            bulletsLeft = magazineSize;
            readyToShoot = true;
        }

        private void Start()
        {
            timer = bulletTimer;
        }

        // Update is called once per frame
        void Update()
        {
            PlayerControls.PlayerStandardActions actions = _playerControls.PlayerStandard;

            if (bulletTimer <= 0)
            {
                if (actions.Gun.triggered)
                {
                    Shoot();
                    bulletTimer = timer;
                }
            }
            bulletTimer -= Time.deltaTime;
        }

        private void Shoot()
        {
           // Raycast ignores layer 8
            int layerMask = ~(1 << 8);
            
            // readyToShoot = false;

            //Find the exact hit position using a raycast
            Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); 
            RaycastHit hit;

            //check if ray hits something
            Vector3 targetPoint;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                Debug.Log(hit.transform.name);
                Debug.Log("OG");
                targetPoint = hit.point;
            }
            else
            {
                Debug.Log("Artifical");
                // targetPoint = ray.GetPoint(75); //Just a point far away from the player
                targetPoint = ray.origin + ray.direction * 10000.0f;
            }
            
            //Calculate direction from attackPoint to targetPoint
            Vector3 directionWithoutSpread = targetPoint - attackPoint.position;
            
            // For debugging bullet path
            Debug.DrawRay(attackPoint.position, directionWithoutSpread, Color.red, 20, false);


            //Calculate spread
            float x = Random.Range(-spread, spread);
            float y = Random.Range(-spread, spread);

            //Calculate new direction with spread
            Vector3 directionWithSpread =
                directionWithoutSpread + new Vector3(x, y, 0); //Just add spread to last direction

            UtilsClass.ShakeCamera(camIntensity, camTimer);
            //Instantiate bullet/projectile
            GameObject currentBullet = 
                Instantiate(bullet, attackPoint.position, Quaternion.identity);

            currentBullet.transform.forward = directionWithSpread.normalized;

            //Add forces to bullet
            currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
            // currentBullet.GetComponent<Rigidbody>().AddForce(directionWithoutSpread.normalized * shootForce, ForceMode.Impulse);
            
            currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);

        }


        
        
    }
}