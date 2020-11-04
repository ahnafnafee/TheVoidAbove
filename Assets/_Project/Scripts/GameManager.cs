using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private float skyboxRotSpeed = 0.4f;

        [Header("UI")] 
        [SerializeField] private GameObject pauseMenu, firstMenu, settingsMenu;
        private PlayerControls playerControls;

        private bool isPaused, isSettings;
        // Start is called before the first frame update
        void Start()
        {
            Time.timeScale = 1;
            isPaused = false;
            isSettings = false;
            pauseMenu.SetActive(false);
            playerControls.UserInterface.Pause.performed += _ => PauseScene();
        }
        
        private void Awake()
        {
            playerControls = new PlayerControls();
            playerControls.Enable();
        }

        // Update is called once per frame
        void Update()
        {
            // Rotates skybox slowly for a dynamic feel
            RenderSettings.skybox.SetFloat("_Rotation", Time.time * skyboxRotSpeed);
        }

        #region Level User Interface

        public void PauseScene()
        {
            Time.timeScale = Convert.ToInt32(isPaused);
            pauseMenu.SetActive(!isPaused);
            isPaused = !isPaused;
            Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        }

        public void SettingsScene()
        {
            firstMenu.SetActive(isSettings);
            settingsMenu.SetActive(!isSettings);

            isSettings = !isSettings;
        }

        public void MenuScene()
        {
            SceneManager.LoadSceneAsync("TitleScreen");
        }

        #endregion
        
    }
}
