//using _Project.Scripts.InputActions;

using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

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
        [SerializeField] private float gunMoveSpeed, maxSpeed, tiltAngle, zSpeed;

        [SerializeField] private float range = 100f;
        
        [SerializeField] private float thrusteraccel;


        [FormerlySerializedAs("thrustermaxspeed")] [SerializeField]
        //Currently unimplemented
        private float thrusterMaxSpeed;

        [Header("Camera Controls")] [SerializeField]
        private Camera mainCam;

        [Header("Respawn Point")] [SerializeField]
        private GameObject respawnPoint,warning;
        
        [Header("Weapon")] public WeaponScript weapon;

        [Header("Player")] private Health pHealth;

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
        }


        void Update()
        {
            #region PlayerRotation

            PlayerControls.PlayerStandardActions actions = _playerControls.PlayerStandard;
            float x_rot = actions.ThrustersX.ReadValue<float>() * tiltAngle;
            float z_rot = actions.ThrustersZ.ReadValue<float>() * tiltAngle;

            Quaternion target = Quaternion.Euler(z_rot, 0, -x_rot);
            transform.rotation = Quaternion.Lerp(transform.rotation, mainCam.transform.rotation * target,
                Time.deltaTime * zSpeed);

            #endregion

            if (actions.PushBack.triggered)
            {
                //When you click LMB, fire the gun (nothing happens like shooting a projectile) and fire the player backwards
                _rb.AddForce(-mainCam.transform.forward * gunMoveSpeed, ForceMode.VelocityChange);
            }
        }


        void FixedUpdate()
        {
            
            PlayerControls.PlayerStandardActions actions = _playerControls.PlayerStandard;

            //Apply forces based on the WASD/spc/shift controls in character controller
            _rb.AddForce(
                thrusteraccel * (actions.ThrustersY.ReadValue<float>() * mainCam.transform.up +
                                 actions.ThrustersX.ReadValue<float>() * mainCam.transform.right +
                                 (actions.ThrustersZ.ReadValue<float>() * mainCam.transform.forward)).normalized,
                ForceMode.Acceleration);


            if (_rb.velocity.magnitude > maxSpeed)
            {
                _rb.velocity = _rb.velocity.normalized * maxSpeed;
            }
        }
        
        void OnCollisionEnter(Collision col)
        {

            //Add anything you want to happen when the player collides in here. It updates akin to Update
            //TODO:Produce some conditionals and tag our assets to discriminate in collisions (I.E.: when they hit a big rock vs a tiny rock, the tiny debris should yield instead of the player)

            if(col.relativeVelocity.magnitude <= safeSpeed)
            {
                Debug.Log("Safe contact.");
            }

            else if(col.relativeVelocity.magnitude > safeSpeed & col.relativeVelocity.magnitude <= dangerSpeed)
            {
                Debug.Log("Bad contact.");
                pHealth.TakeDamage(10);
                Debug.Log(controlsActive);
                StartCoroutine(ControlLockTimer(1));
                Debug.Log(controlsActive);
                Bounce();
            }
            //StartCoroutine(ControlLockTimer()); // Where s = the time in seconds to lock controls for            
            else if (col.relativeVelocity.magnitude > dangerSpeed)
            {
                Debug.Log("LETHAL contact.");
                Debug.Log(controlsActive);
                pHealth.TakeDamage(10);
                StartCoroutine(ControlLockTimer(3));
                Debug.Log(controlsActive);
                Bounce();
            }
            
            void Bounce(){
                foreach (ContactPoint contact in col.contacts)
                {
                    normal += contact.normal;
                }

                normal.Normalize();
                _rb.velocity += Vector3.Reflect(col.relativeVelocity * 0.5f, normal);
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