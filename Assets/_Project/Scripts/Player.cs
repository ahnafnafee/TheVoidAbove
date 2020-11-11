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

        [Header("Respawn Point")] [SerializeField]
        private GameObject respawnPoint,warning;
        
        [Header("Weapon")] public WeaponScript weapon;

        void Awake()
        {
            _playerControls = new PlayerControls();
            _playerControls.Enable();
            _rb = GetComponent<Rigidbody>();
        }
        
        
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
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