//using _Project.Scripts.InputActions;

using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts
{
    public class Player : MonoBehaviour
    {
        PlayerControls _playerControls;

        private Rigidbody _rb;

        [SerializeField] private float gunMoveSpeed, maxSpeed, tiltAngle, zSpeed;

        [SerializeField] private float range = 100f;
        
        [SerializeField] private float thrusteraccel;


        [FormerlySerializedAs("thrustermaxspeed")] [SerializeField]
        //Currently unimplemented
        private float thrusterMaxSpeed;

        [Header("Camera Controls")] [SerializeField]
        private Camera mainCam;

        [SerializeField] private int playerHealth;

        [Header("Respawn Point")] [SerializeField]
        private GameObject respawnPoint,warning;
        
        [Header("Weapon")] public WeaponScript weapon;

        void Awake()
        {
            _playerControls = new PlayerControls();
            _playerControls.Enable();

            _rb = GetComponent<Rigidbody>();
        }

        // Start is called before the first frame update
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Update is called once per frame
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

        public void TakeDamage(int damage)
        {
            if ((playerHealth - damage) > 0)
            {
                playerHealth -= damage;
            }
            else
            {
                transform.position = respawnPoint.transform.position;
            }
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
    }
}