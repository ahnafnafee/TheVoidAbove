using System;
using System.Collections.Generic;
using _Project.Scripts.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

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
        [SerializeField] private GameObject returnHUD;
        [SerializeField] private GameObject D_box;
        [SerializeField] private TextMeshProUGUI levelTime;

        [Header("Sound")]
        [SerializeField] private float mVolume = 100f;
        [SerializeField] private TextMeshProUGUI volumeText;

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

            mVolume = GlobalVar.masterVolume;
            volumeText.text = Convert.ToString(Math.Round(mVolume, 3, MidpointRounding.ToEven));

            Cursor.lockState = CursorLockMode.Locked;
            screenshot = GetComponent<TakeScreenshot>();
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
            //RenderSettings.skybox.SetFloat(Rotation, Time.time * skyboxRotSpeed);

            theTime += Time.deltaTime * speed;
            var minutes = Mathf.FloorToInt(theTime / 60F);
            var seconds = Mathf.FloorToInt(theTime - minutes * 60);
            levelTime.text = $"{minutes:00}:{seconds:00}";

        }

        public void VolumeControl (float level)
        {
            volumeText.text = Convert.ToString(Math.Round(level, 3, MidpointRounding.ToEven));
            AkSoundEngine.SetRTPCValue("masterVolume", level);
            GlobalVar.masterVolume = level;
            mVolume = level;
        }

        public void PauseScene()
        {
            if (pauseMenu == null) return;
            if (GlobalVar.isWin) return;
            Time.timeScale = Convert.ToInt32(isPaused);
            pauseMenu.SetActive(!isPaused);

            if (returnHUD.activeSelf)
                returnHUD.SetActive(!returnHUD.activeSelf);

            gameHud.SetActive(isPaused);
            inGameUI.SetActive(isPaused);

            GlobalVar.isPaused = !isPaused;

            // TODO: DI_System needs build index check for different scenes
            // Temporary fix below

            if (D_box != null)
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
            AkSoundEngine.PostEvent("stop_event", GameObject.Find("WwiseGlobal"));
            FindObjectOfType<ProgressLoadScene>().LoadScene(SceneManager.GetActiveScene().buildIndex);
            //SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }

        public void LoadLevel()
        {
            // PrefabUtility.SaveAsPrefabAsset(GameObject.FindGameObjectWithTag("Player").gameObject, "Assets/_Project/Prefabs/Player/Player.prefab");
            FindObjectOfType<ProgressLoadScene>().LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            //SceneManager.LoadSceneAsync(4);
        }

        public void MenuScene()
        {
            AkSoundEngine.PostEvent("stop_event", GameObject.Find("WwiseGlobal"));
            SceneManager.LoadSceneAsync(0);
        }

    }
}
