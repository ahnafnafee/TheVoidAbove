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
        public bool controlsActive = true;
        private ContactPoint contact;

        [Header("Player Movement")]
        [SerializeField] private float pushBackSpeed;
        [SerializeField] private float maxSpeed;
        [SerializeField] private float tiltAngle;
        [SerializeField] private float zSpeed;
        [SerializeField] private float range = 100f;
        [SerializeField] private float thrusterAcceleration;
        [SerializeField] private float thrusterMaxSpeed,velocity,velocityThreshold,breakForce;

        [Header("Camera Controls")] [SerializeField]
        private Camera mainCam;

        [Header("Respawn Point")] [SerializeField]
        private GameObject respawnPoint;
        
        [Header("Weapon")] public WeaponScript weapon;
        [SerializeField] GameObject rightClickFX,targetPoint;

        [Header("Player")] private Health pHealth;
        [Tooltip("Image component displaying current health")]
        public Image healthFillImage;

        [Header("Feedback Flash")]
        [SerializeField] private Image feedFlash;

        [SerializeField] private GameObject gameOverUI;
 
        private IEnumerator coroutine;

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
            if (actions.debugStun.triggered)
            {
                debugStun();
            }
            if (actions.PushBack.triggered)
            {
                //If you're not stunned, allow to process
                if (controlsActive)
                {
                    //When you click LMB, fire the gun (nothing happens like shooting a projectile) and fire the player backwards
                    _rb.AddForce(-mainCam.transform.forward * pushBackSpeed, ForceMode.VelocityChange);
                    GameObject fx = Instantiate(rightClickFX, targetPoint.transform.position, targetPoint.transform.rotation);
                    Destroy(fx, 5);
                }
            }

            var tempColor = feedFlash.color;
            tempColor.a = 1f - (pHealth.objectHealth / pHealth.health);
            feedFlash.color = tempColor;

            healthFillImage.fillAmount = pHealth.objectHealth / pHealth.health;
        }


        void FixedUpdate()
        {
            var velocity1 = _rb.velocity;
            velocity = velocity1.magnitude;
            PlayerControls.PlayerStandardActions actions = _playerControls.PlayerStandard;
            Vector3 oppVelo = -velocity1;
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

            if (_rb.velocity.magnitude < velocityThreshold
                && actions.ThrustersY.ReadValue<float>() == 0
                && actions.ThrustersX.ReadValue<float>() == 0
                && actions.ThrustersZ.ReadValue<float>() == 0)
            {
                _rb.AddForce(oppVelo.normalized * breakForce);
            }
        }

        void OnCollisionEnter(Collision col)
        {

            //Add anything you want to happen when the player collides in here. It updates akin to Update()
            if (col.collider.CompareTag("Mine") || col.collider.CompareTag("Mine") || col.collider.CompareTag("Debris"))
            {
                if (col.collider.CompareTag("Mine"))
                {
                    pHealth.TakeDamage(30);
                    _rb.AddExplosionForce(10000f, transform.position, 100);
                    StartCoroutine(ControlLockTimer(0.5f));
                    this.GetComponent<PackageManager>().Drop(col.relativeVelocity);

                    Destroy(col.gameObject);
                    // if (!col.collider.CompareTag("Package"))
                    // {
                    //     StartCoroutine(ControlLockTimer(1.5f));
                    //     Bounce();
                    // }
                    // GetComponent<PackageManager>().Drop(col.relativeVelocity);

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
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("OuterZone"))
            {
                coroutine = gameOver(2);
                StartCoroutine(coroutine);
                transform.Find("PlayerBase").gameObject.SetActive(false);
                Rigidbody _rb = GetComponent<Rigidbody>();
                _rb.velocity = Vector3.zero;
                _rb.angularVelocity = Vector3.zero;
                gameOverUI.SetActive(true);
            }
        }
        
        private IEnumerator ControlLockTimer(float t)
        {
            controlsActive = false;

            yield return new WaitForSeconds(t);

            controlsActive = true;
        }

        private IEnumerator gameOver(float respawnTime)
        {
            yield return new WaitForSeconds(respawnTime);
            transform.Find("PlayerBase").gameObject.SetActive(true);
            Rigidbody _rb = GetComponent<Rigidbody>();
            transform.position = respawnPoint.transform.position;
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
            gameOverUI.SetActive(false);
        }

        private void debugStun()
        {
            Debug.Log("DEBUG Player stunned");
            StartCoroutine(ControlLockTimer(10));
            Debug.Log("DEBUG Forcing package drop");
            this.GetComponent<PackageManager>().Drop(Vector3.zero);
        }
    }
}