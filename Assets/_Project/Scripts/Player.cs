//using _Project.Scripts.InputActions;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
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
        public bool outOfBounds = false;

        private bool inZap = false;
        public bool controlsActive = true;
        private bool dashUsed = false;
        private ContactPoint contact;
        private int nubAmmo;
        [SerializeField] private Animator anim;
        private enum State { Idle, Forward, Backward, Shoot };
        private State state = State.Idle;

        [Header("Player Movement")]
        [SerializeField] private float pushBackSpeed;
        [SerializeField] private float dashSpeed;
        [SerializeField] private float dashCooldown = 2;
        [SerializeField] private float maxSpeed;
        [SerializeField] private float tiltAngle;
        [SerializeField] private float zSpeed;
        [SerializeField] private float range = 100f;
        [SerializeField] private float thrusterAcceleration;
        [SerializeField] private float thrusterMaxSpeed, velocity, velocityThreshold, breakForce;

        [Header("Camera Controls")]
        [SerializeField]
        private Camera mainCam;

        [Header("Respawn Point")]
        [SerializeField]
        private GameObject respawnPoint;

        [Header("Weapon")] public WeaponScript weapon;
        [SerializeField] GameObject rightClickFX, targetPoint;

        [Header("Player")] private Health pHealth;
        [Tooltip("Image component displaying current health")]
        public Image healthFillImage;
        public TextMeshProUGUI healthPct;

        [Header("Feedback Flash")]
        [SerializeField] private Image feedFlash;

        [SerializeField] private GameObject gameOverUI;

        [SerializeField] private GameObject ammoUI;

        private IEnumerator coroutine;

        [Header("Collision Tags")]
        public string[] tagList = { "Mine", "Debris", "MovingDebris" };


        public GameObject getRespawn()
        {
            return respawnPoint;
        }
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
            nubAmmo = 0;
            // ammoUI = GameObject.Find("AmmoDrops");
            ammoUI.GetComponent<UI_AmmoControl>().Display(nubAmmo);

            #region Saving
            PlayerControls.UserInterfaceActions UIactions = _playerControls.UserInterface;

            UIactions.Save.performed += _ => SaveSystem.SaveGame(this);

            UIactions.Load.performed += _ =>
            {
                SaveData data = SaveSystem.LoadGame();

                Vector3 temp = new Vector3(data.Pposition[0], data.Pposition[1], data.Pposition[2]);
                this.transform.position = temp;

                GameObject enemy = GameObject.Find("Enemy");
                Vector3 temp2 = new Vector3(data.Eposition[0], data.Eposition[1], data.Eposition[2]);
                enemy.transform.position = temp2;
            };
            #endregion

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
                    //When you click RMB, fire the gun (nothing happens like shooting a projectile) and fire the player backwards
                    AkSoundEngine.PostEvent("alt_shoot_event", this.gameObject);
                    _rb.AddForce(-mainCam.transform.forward * pushBackSpeed, ForceMode.VelocityChange);
                    GameObject fx = Instantiate(rightClickFX, targetPoint.transform.position, targetPoint.transform.rotation);

                    Destroy(fx, 5);
                }
            }


            var tempColor = feedFlash.color;
            tempColor.a = 1f - (pHealth.objectHealth / pHealth.health);
            feedFlash.color = tempColor;

            healthFillImage.fillAmount = pHealth.objectHealth / pHealth.health;
            healthPct.text = (int)(healthFillImage.fillAmount * 100f) + "%";

            StateChange();
            anim.SetInteger("state", (int)state);
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

            //dash movement section
            if (actions.RightDash.triggered)
            {
                if (controlsActive)
                {
                    if (!dashUsed)
                    {
                        _rb.AddForce(mainCam.transform.right * dashSpeed, ForceMode.VelocityChange);
                        StartCoroutine(Dashtimer(dashCooldown));
                    }
                }
            }
            if (actions.LeftDash.triggered)
            {
                if (controlsActive)
                {
                    if (!dashUsed)
                    {
                        _rb.AddForce(-mainCam.transform.right * dashSpeed, ForceMode.VelocityChange);
                        StartCoroutine(Dashtimer(dashCooldown));
                    }
                }
            }
            if (actions.ForeDash.triggered)
            {
                if (controlsActive)
                {
                    if (!dashUsed)
                    {
                        _rb.AddForce(mainCam.transform.forward * pushBackSpeed, ForceMode.VelocityChange);
                        StartCoroutine(Dashtimer(dashCooldown));
                    }
                }
            }
            if (actions.AftDash.triggered)
            {
                if (controlsActive)
                {
                    if (!dashUsed)
                    {
                        _rb.AddForce(-mainCam.transform.forward * pushBackSpeed, ForceMode.VelocityChange);
                        StartCoroutine(Dashtimer(dashCooldown));
                    }
                }
            }
            if (actions.UpDash.triggered)
            {
                if (controlsActive)
                {
                    if (!dashUsed)
                    {
                        _rb.AddForce(mainCam.transform.up * pushBackSpeed, ForceMode.VelocityChange);
                        StartCoroutine(Dashtimer(dashCooldown));
                    }
                }
            }
            if (actions.DownDash.triggered)
            {
                if (controlsActive)
                {
                    if (!dashUsed)
                    {
                        _rb.AddForce(-mainCam.transform.up * pushBackSpeed, ForceMode.VelocityChange);
                        StartCoroutine(Dashtimer(dashCooldown));
                    }
                }
            }

            // Braking velocity
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
            var match = tagList
                .FirstOrDefault(stringToCheck => stringToCheck.Contains(col.gameObject.tag));


            // Debug.Log($"Tag: {col.gameObject.tag}");

            // Add required tags in the declaration of tagList
            if (match != null)
            {
                if (col.collider.CompareTag("Mine"))
                {
                    pHealth.TakeDamage(30,false);
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
                    AkSoundEngine.PostEvent("impact_event", this.gameObject);
                    Debug.Log("Bad contact.");
                    pHealth.TakeDamage(5,false);
                    if (!col.collider.CompareTag("Package"))
                    {
                        StartCoroutine(ControlLockTimer(0.45f));
                        Bounce(col);
                    }
                    this.GetComponent<PackageManager>().Drop(col.relativeVelocity);
                }
                //StartCoroutine(ControlLockTimer()); // Where s = the time in seconds to lock controls for            
                else if (col.relativeVelocity.magnitude > dangerSpeed)
                {
                    AkSoundEngine.PostEvent("hard_impact_event", this.gameObject);
                    Debug.Log("LETHAL contact.");
                    pHealth.TakeDamage(10,false);
                    if (!col.collider.CompareTag("Package"))
                    {
                        StartCoroutine(ControlLockTimer(0.9f));
                        Bounce(col);
                    }
                    this.GetComponent<PackageManager>().Drop(col.relativeVelocity);
                }

                // void Bounce()
                // {
                //     foreach (ContactPoint contact in col.contacts)
                //     {
                //         normal += contact.normal;
                //     }
                //
                //     normal.Normalize();
                //     _rb.velocity += Vector3.Reflect(-col.relativeVelocity * 0.4f, normal);
                // }
                //_rb.velocity = Vector3.Reflect(_rb.velocity, normal);
            }
        }

        private void Bounce(Collision col)
        {
            // Debug.Log("Bouncing");
            foreach (ContactPoint ct in col.contacts)
            {
                normal += ct.normal;
            }

            normal.Normalize();
            _rb.velocity += Vector3.Reflect(-col.relativeVelocity * 0.4f, normal);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("OuterZone"))
            {
                Debug.Log("Starting timer to damage");
                outOfBounds = true;
                StartCoroutine(BoundsTimer());

                /*transform.Find("PlayerBase").gameObject.SetActive(false);
                Rigidbody _rb = GetComponent<Rigidbody>();
                _rb.velocity = Vector3.zero;
                _rb.angularVelocity = Vector3.zero;
                gameOverUI.SetActive(true);*/
            }

            if (other.gameObject.name == "Zap_Zone")
            {
                inZap = false;
                StopCoroutine(SwiftDeath(25));

            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("OuterZone"))
            {
                StopCoroutine(DrainHealth(5));
                outOfBounds = false;
            }

            if (other.gameObject.name == "Zap_Zone")
            {
                inZap = true;
                StartCoroutine(SwiftDeath(25));

            }
        }

        private IEnumerator SwiftDeath(int damage)
        {
            while (inZap)
            {
                Debug.Log("Damage.");
                pHealth.TakeDamage(damage,false);
                yield return new WaitForSeconds(1);
            }
        }

        private IEnumerator ControlLockTimer(float t)
        {
            controlsActive = false;

            yield return new WaitForSeconds(t);

            controlsActive = true;
        }

        private IEnumerator Dashtimer(float t)
        {
            Debug.Log("Dash has been used");
            dashUsed = true;

            yield return new WaitForSeconds(t);

            Debug.Log("Dash is ready");
            dashUsed = false;
        }

        private IEnumerator BoundsTimer()
        {
            yield return new WaitForSeconds(5);
            Debug.Log("Time's up");
            StartCoroutine(DrainHealth(5));

        }

        private IEnumerator DrainHealth(int damage)
        {
            while (outOfBounds)
            {
                Debug.Log("Damage.");
                pHealth.TakeDamage(damage,false);
                yield return new WaitForSeconds(1);
            }
        }

        private void debugStun()
        {
            Debug.Log("DEBUG Player stunned");
            StartCoroutine(ControlLockTimer(10));
            Debug.Log("DEBUG Forcing package drop");
            this.GetComponent<PackageManager>().Drop(Vector3.zero);
        }
        public bool isPowered()
        {
            if (nubAmmo > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void powerUp()
        {
            nubAmmo = 5;
            ammoUI.GetComponent<UI_AmmoControl>().Display(nubAmmo);
        }

        public void Shoot()
        {
            if (nubAmmo > 0)
            {
                nubAmmo -= 1;
                ammoUI.GetComponent<UI_AmmoControl>().Display(nubAmmo);
            }

        }

        // Player animation states
        private void StateChange()
        {
            PlayerControls.PlayerStandardActions actions = _playerControls.PlayerStandard;


            if (actions.ThrustersZ.ReadValue<float>() > 0)
            {
                state = State.Forward;
            }
            else if (actions.ThrustersZ.ReadValue<float>() < 0)
            {
                state = State.Backward;
            }
            else if (actions.Gun.triggered)
            {
                state = State.Shoot;
            }
            else if (_rb.velocity.magnitude < 5.0f)
            {
                state = State.Idle;
            }
        }
    }
}