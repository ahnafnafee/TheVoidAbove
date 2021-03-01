using System;
using _Project.Scripts.Utils;
using TMPro;
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
        [SerializeField] private GameObject inGameUI;
        [SerializeField] private GameObject D_box;

        [SerializeField] private TextMeshProUGUI levelTime;

        float theTime = 0f;
        float speed = 1;

        private bool isPaused, isSettings;

        private static readonly int Rotation = Shader.PropertyToID("_Rotation");

        // Start is called before the first frame update
        void Start()
        {
            Time.timeScale = 1;
            theTime = 0;
            isPaused = false;
            isSettings = false;
            pauseMenu.SetActive(false);
            gameHud.SetActive(true);
            inGameUI.SetActive(true);
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

            theTime += Time.deltaTime * speed;
            string minutes = Mathf.Floor((theTime % 3600) / 60).ToString("00");
            string seconds = (theTime % 60).ToString("00");
            levelTime.text = minutes + ":" + seconds;

        }

        #region Level User Interface

        public void PauseScene()
        {
            if (pauseMenu == null) return;
            Time.timeScale = Convert.ToInt32(isPaused);
            pauseMenu.SetActive(!isPaused);
            gameHud.SetActive(isPaused);
            inGameUI.SetActive(isPaused);

            GlobalVar.isPaused = !isPaused;

            // TODO: DI_System needs build index check for different scenes
            // Temporary fix below

            D_box.GetComponent<CanvasGroup>().alpha = Convert.ToInt32(isPaused);
            
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
            FindObjectOfType<ProgressLoadScene>().LoadScene(SceneManager.GetActiveScene().buildIndex);
            //SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }

        public void LoadLevel2()
        {
            FindObjectOfType<ProgressLoadScene>().LoadScene(4);
            //SceneManager.LoadSceneAsync(4);
        }

        public void MenuScene()
        {
            SceneManager.LoadSceneAsync(0);
        }

        #endregion
        
    }
}
