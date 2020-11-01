using _Project.Scripts.InputActions;
using UnityEngine;

namespace _Project.Scripts
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private float thrusteraccel;
        [SerializeField]
        //Currently unimplemented
        private float thrustermaxspeed;
        [SerializeField]
        private float gunmovespeed,rotSpeed,maxSpeed;
        [SerializeField]
        private int playerHealth;

        private Rigidbody _rb;
        PlayerControls _playerControls;

        [Header("Respawn Point")] [SerializeField]
        private GameObject respawnPoint;
    
        [Header("Camera Controls")] 
        [SerializeField] private Camera mainCam;
        [SerializeField] private float lSpeed = 1f;

        void Awake()
        {
            _playerControls = new PlayerControls();
            _playerControls.Enable();

            _rb = GetComponent<Rigidbody>();
        }

        // Start is called before the first frame update
        void Start()
        {
        
        
        }
    
        // Update is called once per frame
        void Update()
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, mainCam.transform.rotation, Time.deltaTime * lSpeed);
        }
    

        void FixedUpdate()
        {
            PlayerControls.PlayerStandardActions actions = _playerControls.PlayerStandard;

            //Apply forces based on the wasd/spc/shift controls in character controller
            _rb.AddForce(thrusteraccel * (actions.ThrustersY.ReadValue<float>()* mainCam.transform.up +  actions.ThrustersX.ReadValue<float>() * mainCam.transform.right + (actions.ThrustersZ.ReadValue<float>() * mainCam.transform.forward)).normalized, ForceMode.Acceleration);

            if (_rb.velocity.magnitude > maxSpeed)
            {
                _rb.velocity = _rb.velocity.normalized * maxSpeed;
            }
        
            //When you click LMB, fire the gun (nothing happens like shooting a projectile) and fire the player backwards
            if (actions.Gun.triggered)
                Shoot();

        }

        void Shoot()
        {
            _rb.AddForce(-mainCam.transform.forward * gunmovespeed, ForceMode.VelocityChange);
        }

        public void TakeDamage(int damage)
        {
            if ((playerHealth - damage) > 0)
            {
                playerHealth -= damage;
            }
            else
            {
                print("You lose");
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("OuterZone"))
            {
                transform.position = respawnPoint.transform.position;
            }
        }
    }
}
