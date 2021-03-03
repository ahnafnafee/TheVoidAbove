using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts
{
    public class ButtonManager : MonoBehaviour
    {
        public void MainMenu()
        {
            SceneManager.LoadScene(0);
        }

        public void Tutorial()
        {
            SceneManager.LoadScene(1);
        }

        public void StartGame()
        {
            SceneManager.LoadScene(2);
            AkSoundEngine.PostEvent("stop_event", GameObject.Find("WwiseGlobal"));
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}
