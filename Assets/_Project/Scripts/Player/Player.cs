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
        private TrailRenderer dashTrail;
        [SerializeField] private Animator anim;
        public enum State { Idle, Forward, Backward, Shoot, FS, BS };
        public State state = State.Idle;
        public State lastState;
        private float transitionSpeed = 0.1f;

        [Header("Player Movement")]
        [SerializeField] private float dashSpeed = 70f;
        [SerializeField] private float dashCooldown = 1.5f;
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
        [SerializeField] GameObject targetPoint;

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

        [Header("UI")]
        [SerializeField] private GameObject returnHUD;

        [Header("Dash")]
        [Tooltip("Image component displaying dash")]
        [SerializeField] private Image dashFillImage;
        [SerializeField] private float dashTime = 3.0f;
        private float dTimer;
        Vector2 input;
        public float lerpDuration = .2f;
        private float startValue = 0;
        private float endValue = 10;



        public GameObject getRespawn()
        {
            return respawnPoint;
        }
        void Awake()
        {
            _playerControls = new PlayerControls();
            _playerControls.Enable();
            _rb = GetComponent<Rigidbody>();
            dashTrail = GameObject.Find("chest").GetComponent<TrailRenderer>();
        }


        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            pHealth = GetComponent<Health>();
            nubAmmo = 0;
            // ammoUI = GameObject.Find("AmmoDrops");
            ammoUI.GetComponent<AmmoControlUI>().Display(nubAmmo);
            returnHUD.SetActive(false);

            // Dash Init
            dTimer = dashTime;

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
            PlayerControls.PlayerStandardActions actions = _playerControls.PlayerStandard;

            /*input.x = actions.ThrustersX.ReadValue<float>();
            input.y = actions.ThrustersZ.ReadValue<float>();*/

            //Debug.Log(input);
            /*StartCoroutine(Lerp(input.x, true));
            StartCoroutine(Lerp(input.y, false));
            anim.SetFloat("inputx", input.x);
            anim.SetFloat("inputy", input.y);*/

            #region PlayerRotation

            var lookPos = mainCam.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);

            float xRot = actions.ThrustersX.ReadValue<float>() * tiltAngle;
            float zRot = actions.ThrustersZ.ReadValue<float>() * tiltAngle;

            Quaternion target = Quaternion.Euler(-xRot, 0, -zRot);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation * Quaternion.AngleAxis(90f, Vector3.up) * target,
                Time.deltaTime * zSpeed);

            #endregion

            // DashTimer();
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
            if (actions.Dash.triggered)
            {
                //If you're not stunned, allow to process
                if (controlsActive)
                {
                    if (!dashUsed)
                    {
                        _rb.velocity = Vector3.zero;
                        Vector3 dashdir = (actions.ThrustersY.ReadValue<float>() * mainCam.transform.up + actions.ThrustersX.ReadValue<float>() * mainCam.transform.right + (actions.ThrustersZ.ReadValue<float>() * mainCam.transform.forward)).normalized;
                        AkSoundEngine.PostEvent("alt_shoot_event", this.gameObject);

                        if (dashdir != Vector3.zero)
                        {
                            _rb.AddForce(dashdir * dashSpeed, ForceMode.VelocityChange);
                            // dashUsed = true;
                            // DashTimer(3);
                            StartCoroutine(DashTimer(dashTime));
                            StartCoroutine(dashTrails(dashTrail));
                        }
                        else
                        {
                            _rb.AddForce(-mainCam.transform.forward * dashSpeed, ForceMode.VelocityChange);
                            // dashUsed = true;
                            // DashTimer(3);
                            StartCoroutine(DashTimer(dashTime));
                            StartCoroutine(dashTrails(dashTrail));
                        }

                    }
                }
            }

            if (actions.ManualBrake.ReadValue<float>().Equals(1))
            {
                _rb.AddForce(-_rb.velocity * 1f, ForceMode.Acceleration);
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

        IEnumerator Lerp(float valueToLerp, bool isX)
        {
            float timeElapsed = 0;
            endValue = -valueToLerp;

            while (valueToLerp != endValue)
            {
                //valueToLerp -= .05f;
                endValue = Mathf.Lerp(endValue, valueToLerp, timeElapsed / lerpDuration);
                timeElapsed += Time.deltaTime;
                Debug.Log(endValue);
                if (isX) {
                    anim.SetFloat("inputx", endValue);
                }else
                {
                    anim.SetFloat("inputy", endValue);
                }

                yield return null;
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
                    StartCoroutine(ControlLockTimer(2f));
                    this.GetComponent<PackageManager>().Drop(col.relativeVelocity);

                    Destroy(col.gameObject);

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
                        StartCoroutine(ControlLockTimer(1f));
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
                        StartCoroutine(ControlLockTimer(3f));
                        Bounce(col);
                    }
                    this.GetComponent<PackageManager>().Drop(col.relativeVelocity);
                }

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
                // Debug.Log("Starting timer to damage");
                returnHUD.SetActive(true);
                outOfBounds = true;
                StartCoroutine(BoundsTimer());

            }

            if (other.gameObject.name.Contains("Zap_Zone"))
            {
                inZap = false;
                StopCoroutine(SwiftDeath(25));

            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("CheckPoint"))
            {
                GetComponent<Health>().UpdateRespawnPoint(other.gameObject);
            }
            if (other.CompareTag("laser"))
            {
                pHealth.TakeDamage(10);
            }
            if (other.CompareTag("OuterZone"))
            {
                returnHUD.SetActive(false);
                StopCoroutine(DrainHealth(5));
                outOfBounds = false;
            }

            if (other.gameObject.name.Contains("Zap_Zone"))
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
            tiltAngle = 0;

            yield return new WaitForSeconds(t);

            controlsActive = true;
            tiltAngle = 4;
        }

        // Had to change it to traditional method to update UI - Ahnaf

        private IEnumerator DashTimer(float t)
        {
            Debug.Log("Dash has been used");
            dashUsed = true;

            float timer;

            for (timer = t; timer >= 0; timer -= Time.deltaTime)
            {
                dashFillImage.fillAmount = (dTimer - timer) / dTimer;
                yield return null;
            }

            dashFillImage.fillAmount = 1;
            Debug.Log("Dash is ready");
            dashUsed = false;
        }

        // private void DashTimer()
        // {
        //     if (dashUsed)
        //     {
        //         if (dashTime <= 0)
        //         {
        //             dashFillImage.fillAmount = 1;
        //             dashTime = dTimer;
        //             dashUsed = false;
        //             return;
        //         }
        //         dashTime -= Time.deltaTime;
        //         dashFillImage.fillAmount = (dTimer - dashTime) / dTimer;
        //     }
        // }

        private IEnumerator dashTrails(TrailRenderer trail)
        {
            trail.time = 0.3f;
            yield return new WaitForSeconds(0.3f);
            trail.time = 0;
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
            ammoUI.GetComponent<AmmoControlUI>().Display(nubAmmo);
        }

        public void Shoot()
        {
            if (nubAmmo > 0)
            {
                nubAmmo -= 1;
                ammoUI.GetComponent<AmmoControlUI>().Display(nubAmmo);
            }

        }



        // Player animation states
        private void StateChange()
        {
            PlayerControls.PlayerStandardActions actions = _playerControls.PlayerStandard;


            if (actions.ThrustersZ.ReadValue<float>() > 0)
            {
                state = State.Forward;
                lastState = State.Forward;
                if (actions.Gun.triggered)
                {
                    state = State.FS;
                }
            }
            else if (actions.ThrustersZ.ReadValue<float>() < 0)
            {
                state = State.Backward;
                lastState = State.Backward;
                if (actions.Gun.triggered)
                {
                    state = State.BS;
                }
            }
            else if ((_rb.velocity.magnitude > 2.0f && lastState == State.Forward))
            {
                state = State.Forward;
            }
            else if ((_rb.velocity.magnitude > 2.0f && lastState == State.Backward))
            {
                state = State.Backward;
            }
            else
            {
                if (_rb.velocity.magnitude < 2.0f)
                {
                    lastState = State.Idle;
                    state = State.Idle;

                }
            }

            if (actions.Gun.triggered)
            {
                if (lastState == State.Forward)
                {
                    state = State.FS;
                }
                else if (lastState == State.Backward)
                {
                    state = State.BS;
                }
                else if (lastState == State.Idle)
                {
                    state = State.Shoot;
                }
                else
                {
                    state = State.Shoot;
                }
            }

        }
    }
}