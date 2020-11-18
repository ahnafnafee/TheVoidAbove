//using _Project.Scripts.InputActions;

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts
{
    public class PlayerWin : MonoBehaviour
    {
        [SerializeField] private GameObject winUI;
        private PlayerControls playerControls;
        public PackageManager pkgManager;
        [SerializeField] private int nextScene;
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
                winUI.SetActive(true);
                if (nextScene != 4)
                    StartCoroutine(LoadScene());
            }
        }

        IEnumerator LoadScene()
        {
            yield return new WaitForSeconds(2.5f);
            SceneManager.LoadScene(nextScene);
        }
    }
}
