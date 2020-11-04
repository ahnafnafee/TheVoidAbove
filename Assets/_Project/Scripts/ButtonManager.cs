using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void startGame()
    {
        SceneManager.LoadScene(1);
    }
    public void tutorial()
    {
        SceneManager.LoadScene(2);
    }

    public void mainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void exitGame()
    {
        Application.Quit();
    }
}
