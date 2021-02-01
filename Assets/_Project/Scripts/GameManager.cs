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
        
        [Header("Screenshot")] private TakeScreenshot screenshot;
        [SerializeField] private GameObject gameHud;

        private bool isPaused, isSettings;

        private static readonly int Rotation = Shader.PropertyToID("_Rotation");

        // Start is called before the first frame update
        void Start()
        {
            Time.timeScale = 1;
            isPaused = false;
            isSettings = false;
            pauseMenu.SetActive(false);
            gameHud.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            screenshot = GetComponent<TakeScreenshot>();
            playerControls.UserInterface.Pause.performed += _ => PauseScene();
            playerControls.UserInterface.Screenshot.performed += _ => TakeScreenshot();
        }

        private void TakeScreenshot()
        {
            screenshot.OnTakeScreenshotButtonPressed();
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
            //RenderSettings.skybox.SetFloat(Rotation, Time.time * skyboxRotSpeed);
        }

        #region Level User Interface

        public void PauseScene()
        {
            Time.timeScale = Convert.ToInt32(isPaused);
            pauseMenu.SetActive(!isPaused);
            gameHud.SetActive(isPaused);
            isPaused = !isPaused;
            Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        }

        public void SettingsScene()
        {
            firstMenu.SetActive(isSettings);
            settingsMenu.SetActive(!isSettings);

            isSettings = !isSettings;
        }

        public void RestartScene()
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        }

        public void LoadLevel2()
        {
            SceneManager.LoadSceneAsync(3);
        }

        public void MenuScene()
        {
            SceneManager.LoadSceneAsync("TitleScreen");
        }

        #endregion
        
    }
}
