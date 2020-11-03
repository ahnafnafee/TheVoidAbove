//using _Project.Scripts.InputActions;

using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts
{
    public class Player : MonoBehaviour
    {
        PlayerControls _playerControls;

        private Rigidbody _rb;

        [SerializeField] private float gunMoveSpeed, rotSpeed, maxSpeed, tiltAngle, zSpeed;

        [SerializeField] private float lSpeed = 1f, range = 100f;
        
        [SerializeField] private float thrusteraccel;

        public GameObject muzzlePos;

        [FormerlySerializedAs("thrustermaxspeed")] [SerializeField]
        //Currently unimplemented
        private float thrusterMaxSpeed;

        [Header("Camera Controls")] [SerializeField]
        private Camera mainCam;

        [SerializeField] private int playerHealth;

        [Header("Respawn Point")] [SerializeField]
        private GameObject respawnPoint;
        
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
            transform.rotation = Quaternion.Slerp(transform.rotation, mainCam.transform.rotation * target,
                Time.deltaTime * zSpeed);

            #endregion

            if (actions.PushBack.triggered)
            {
                //When you click LMB, fire the gun (nothing happens like shooting a projectile) and fire the player backwards
                _rb.AddForce(-mainCam.transform.forward * gunMoveSpeed, ForceMode.VelocityChange);
            }

            // Shooting mechanics
            if (actions.Gun.triggered)
            {
                weapon.Shoot(SpawnPos(), mainCam.transform.rotation, true);
            }
        }


        void FixedUpdate()
        {
            PlayerControls.PlayerStandardActions actions = _playerControls.PlayerStandard;

            //Apply forces based on the wasd/spc/shift controls in character controller
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

        void Shoot()
        {
            RaycastHit hit;
            if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit, range))
            {
                Debug.Log(hit.transform.name);
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

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("OuterZone"))
            {
                transform.position = respawnPoint.transform.position;
            }
        }

        Vector3 SpawnPos()
        {
            return muzzlePos.transform.position;
        }
    }
}