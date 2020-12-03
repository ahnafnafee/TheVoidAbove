//using _Project.Scripts.InputActions;

using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Project.Scripts
{
    public class Player : MonoBehaviour
    {
        PlayerControls _playerControls;

        private Rigidbody _rb;
        
        [Header("Player Collision")]
        Vector3 normal;
        //The speed where nothing happens to the player on collision; a safe contact with a surface
        [SerializeField] float safeSpeed = 25;
        //The speed where problems occur; between safe and danger is a dangerous hit, above is a lethal hit that's more severe
        [SerializeField] float dangerSpeed = 50;
        bool controlsActive = true;
        private ContactPoint contact;

        [Header("Player Movement")]
        [SerializeField] private float pushBackSpeed;
        [SerializeField] private float maxSpeed;
        [SerializeField] private float tiltAngle;
        [SerializeField] private float zSpeed;
        [SerializeField] private float range = 100f;
        [SerializeField] private float thrusterAcceleration;
        [SerializeField] private float thrusterMaxSpeed;

        [Header("Camera Controls")] [SerializeField]
        private Camera mainCam;

        [Header("Respawn Point")] [SerializeField]
        private GameObject respawnPoint,warning;
        
        [Header("Weapon")] public WeaponScript weapon;
        [SerializeField] GameObject rightClickFX,targetPoint;

        [Header("Player")] private Health pHealth;
        [Tooltip("Image component displaying current health")]
        public Image healthFillImage;

        [Header("GUI")] [SerializeField] private GameObject pkgPointer;

        void Awake()
        {
            _playerControls = new PlayerControls();
            _playerControls.Enable();
            _rb = GetComponent<Rigidbody>();
        }
        
        
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            pHealth = GetComponent<Health>();
            pkgPointer.SetActive(true);
            
        }


        void Update()
        {
            #region PlayerRotation
            
            var lookPos = mainCam.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);

            PlayerControls.PlayerStandardActions actions = _playerControls.PlayerStandard;
            float xRot = actions.ThrustersX.ReadValue<float>() * tiltAngle;
            float zRot = actions.ThrustersZ.ReadValue<float>() * tiltAngle;

            Quaternion target = Quaternion.Euler(-xRot, 0, -zRot);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation * Quaternion.AngleAxis(90f, Vector3.up) * target,
                Time.deltaTime * zSpeed);

            #endregion

            if (actions.PushBack.triggered)
            {
                Debug.Log("Asdas");
                //When you click LMB, fire the gun (nothing happens like shooting a projectile) and fire the player backwards
                _rb.AddForce(-mainCam.transform.forward * pushBackSpeed, ForceMode.VelocityChange);
                GameObject fx = Instantiate(rightClickFX, targetPoint.transform.position, targetPoint.transform.rotation);
                Destroy(fx, 5);
            }
            
            healthFillImage.fillAmount = pHealth.objectHealth / pHealth.health;
        }


        void FixedUpdate()
        {

            PlayerControls.PlayerStandardActions actions = _playerControls.PlayerStandard;

            //Apply forces based on the WASD/spc/shift controls in character controller 
            if (controlsActive)
            {
                //Set a var equal to the controls
                Vector3 thrusterForce = thrusterAcceleration * (actions.ThrustersY.ReadValue<float>() * mainCam.transform.up +
                                     actions.ThrustersX.ReadValue<float>() * mainCam.transform.right +
                                     (actions.ThrustersZ.ReadValue<float>() * mainCam.transform.forward)).normalized;

                //If you're going at max speed, don't contribute to the main direction, but allow other directions via projectonplane
                if (_rb.velocity.magnitude >= thrusterMaxSpeed && Vector3.Dot(thrusterForce, _rb.velocity) > 0)
                {
                    thrusterForce = Vector3.ProjectOnPlane(thrusterForce, _rb.velocity);
                }

                //Apply the force variable
                _rb.AddForce(thrusterForce, ForceMode.Acceleration);
            }

            //If you're at top speed, don't go any faster
            if (_rb.velocity.magnitude > maxSpeed)
            {
                _rb.velocity = _rb.velocity.normalized * maxSpeed;
            }
        }

        void OnCollisionEnter(Collision col)
        {

            //Add anything you want to happen when the player collides in here. It updates akin to Update()
            //TODO:Produce some conditionals and tag our assets to discriminate in collisions (I.E.: when they hit a big rock vs a tiny rock, the tiny debris should yield instead of the player)

            if (col.collider.CompareTag("Mine"))
            {
                pHealth.TakeDamage(30);
                Destroy(col.gameObject);
                
            }
            if (col.relativeVelocity.magnitude <= safeSpeed)
            {
                Debug.Log("Safe contact.");
            }

            else if (col.relativeVelocity.magnitude > safeSpeed & col.relativeVelocity.magnitude <= dangerSpeed)
            {
                Debug.Log("Bad contact.");
                pHealth.TakeDamage(5);
                if (!col.collider.CompareTag("Package"))
                {
                    StartCoroutine(ControlLockTimer(0.5f));
                    Bounce();
                }
                this.GetComponent<PackageManager>().Drop(col.relativeVelocity);
            }
            //StartCoroutine(ControlLockTimer()); // Where s = the time in seconds to lock controls for            
            else if (col.relativeVelocity.magnitude > dangerSpeed)
            {
                Debug.Log("LETHAL contact.");
                pHealth.TakeDamage(10);
                if (!col.collider.CompareTag("Package"))
                {
                    StartCoroutine(ControlLockTimer(2));
                    Bounce();
                }
                this.GetComponent<PackageManager>().Drop(col.relativeVelocity);
            }

            void Bounce()
            {
                foreach (ContactPoint contact in col.contacts)
                {
                    normal += contact.normal;
                }

                normal.Normalize();
                _rb.velocity += Vector3.Reflect(-col.relativeVelocity * 0.4f, normal);
            }
            //_rb.velocity = Vector3.Reflect(_rb.velocity, normal);

        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("warning"))
            {
                warning.SetActive(true);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("warning"))
            {
                warning.SetActive(false);            
            }
            if (other.CompareTag("OuterZone"))
            {
                transform.position = respawnPoint.transform.position;
                transform.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                _rb.velocity = Vector3.zero;
                _rb.angularVelocity = Vector3.zero;
            }
        }
        
        private IEnumerator ControlLockTimer(float t)
        {
            controlsActive = false;

            yield return new WaitForSeconds(t);

            controlsActive = true;
        }
    }
}