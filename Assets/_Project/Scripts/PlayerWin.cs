//using _Project.Scripts.InputActions;

using System;
using System.Collections;
using _Project.Scripts.Utils;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace _Project.Scripts
{
    public class PlayerWin : MonoBehaviour
    {
        private PlayerControls playerControls;
        public PackageManager pkgManager;
        [SerializeField] private int nextScene;
        [SerializeField] private GameObject levelChangeInterface;
        [SerializeField] private GameObject gameHud;
        [SerializeField] private GameObject inGameUI;
        [SerializeField] private GameObject returnHUD;
        [SerializeField] private GameObject D_box;
        [SerializeField] private CinemachineFreeLook _freeLookComponent;

        private const double k_Tolerance = 0.001f;
        private void Awake()
        {
            playerControls = new PlayerControls();
            playerControls.Enable();
        }

        private void Start()
        {
            _freeLookComponent.m_Lens.FieldOfView = 60;
            GlobalVar.isPaused = false;
            GlobalVar.isWin = false;
            playerControls.UserInterface.Restart.performed += _ => RestartScene();
        }

        void RestartScene()
        {
            AkSoundEngine.PostEvent("stop_event", GameObject.Find("WwiseGlobal"));
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        }

        private void OnEnable()
        {
            playerControls.Enable();
        }
    
        private void OnDisable()
        {
            playerControls.Disable();
        }

        void OnTriggerEnter(Collider other)
        {
            if (pkgManager.hasPackage && other.transform.CompareTag("Player"))
            {
                if (nextScene != 4)
                    StartCoroutine(LoadInterface(2, 140));
            }
        }

        IEnumerator LoadInterface(float duration, float value)
        {
            var t = 0.0f;
            var startFoV = _freeLookComponent.m_Lens.FieldOfView;
            while (Math.Abs(t - duration) > k_Tolerance) {
                t += Time.deltaTime;
                if (t > duration) t = duration;
                _freeLookComponent.m_Lens.FieldOfView = Mathf.Lerp(startFoV, value, t / duration);
                yield return null;
            }


            // _freeLookComponent.m_Lens.FieldOfView = 140;
            GlobalVar.isWin = true;
            // yield return new WaitForSeconds(3f);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;

            if (returnHUD.activeSelf)
                returnHUD.SetActive(!returnHUD.activeSelf);

            gameHud.SetActive(false);
            inGameUI.SetActive(false);

            if (D_box != null)
                D_box.GetComponent<CanvasGroup>().alpha = Convert.ToInt32(false);
            levelChangeInterface.SetActive(true);
            AkSoundEngine.PostEvent("stop_event", this.gameObject);
        }
    }
}
