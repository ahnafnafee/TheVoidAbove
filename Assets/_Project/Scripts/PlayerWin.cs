//using _Project.Scripts.InputActions;

using System.Collections;
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
        private void Awake()
        {
            playerControls = new PlayerControls();
            playerControls.Enable();
        }

        private void Start()
        {
            playerControls.UserInterface.Restart.performed += _ => RestartScene();
        }

        void RestartScene()
        {
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
                    StartCoroutine(LoadInterface());
            }
        }

        IEnumerator LoadInterface()
        {
            yield return new WaitForSeconds(3f);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            gameHud.SetActive(false);
            inGameUI.SetActive(false);
            levelChangeInterface.SetActive(true);
        }
    }
}
