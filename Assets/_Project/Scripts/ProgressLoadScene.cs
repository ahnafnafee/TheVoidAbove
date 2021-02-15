using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Project.Scripts
{
    public class ProgressLoadScene : MonoBehaviour
    {
        // [SerializeField]
        // private Text progressText;
        [SerializeField]
        private Slider slider;

        private AsyncOperation operation;
        private Canvas canvas;

        private void Awake()
        {
            canvas = GetComponentInChildren<Canvas>(true);
            DontDestroyOnLoad(gameObject);
        }

        public void LoadScene(int index)
        {
            UpdateProgressUI(0);
            canvas.gameObject.SetActive(true);

            StartCoroutine(BeginLoad(index));
        }

        private IEnumerator BeginLoad(int index)
        {
            operation = SceneManager.LoadSceneAsync(index);

            while (!operation.isDone)
            {
                UpdateProgressUI(operation.progress);
                yield return null;
            }

            UpdateProgressUI(operation.progress);
            operation = null;
            canvas.gameObject.SetActive(false);
        }

        private void UpdateProgressUI(float progress)
        {
            slider.value = progress;
            // progressText.text = (int)(progress * 100f) + "%";
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
