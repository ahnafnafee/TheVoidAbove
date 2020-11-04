//using _Project.Scripts.InputActions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts
{
    public class PlayerWin : MonoBehaviour
    {
        [SerializeField] private GameObject winUI;
        private PlayerControls playerControls;
        private void Awake()
        {
            playerControls = new PlayerControls();
            playerControls.Enable();
        }

        private void Start()
        {
            playerControls.RestartMap.restart.performed += _ => RestartScene();
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
            if (other.transform.tag.Equals("Package"))
            {
                winUI.SetActive(true);
            }
        }
    }
}
