using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Utils;
using UnityEngine;

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
        
        public float camIntensity, camTimer;

        //Reference
        public Camera fpsCam;

        public int magazineSize, bulletsPerTap;

        //bullet force
        public float shootForce, upwardForce;

        //bools
        bool shooting, readyToShoot, reloading;

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

        private void Shoot()
        {
            // readyToShoot = false;

            //Find the exact hit position using a raycast
            Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); 
            RaycastHit hit;

            //check if ray hits something
            Vector3 targetPoint;
            if (Physics.Raycast(ray, out hit))
                targetPoint = hit.point;
            else
            {
                // targetPoint = ray.GetPoint(75); //Just a point far away from the player
                targetPoint = ray.origin + ray.direction * 10000.0f;
            }

            //Calculate direction from attackPoint to targetPoint
            Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

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
            currentBullet.GetComponent<Rigidbody>()
                .AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
            currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);

        }


        // Update is called once per frame
        void Update()
        {
            PlayerControls.PlayerStandardActions actions = _playerControls.PlayerStandard;
            if (actions.Gun.triggered)
            {
                Shoot();
            }
        }
        
    }
}