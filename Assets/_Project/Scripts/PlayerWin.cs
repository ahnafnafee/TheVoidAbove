using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerWin : MonoBehaviour
{
    // [SerializeField] private GameObject winUI;
    // [SerializeField] private SceneActions restart;
    private void Awake()
    {
        // restart.restartMap.restart.performed += _ => restartScene();
    }
    
    // void restartScene()
    // {
    //     SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    // }
    //
    // private void OnEnable()
    // {
    //     restart.Enable();
    // }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.Equals("Package"))
        {
            // winUI.SetActive(true);
            print("You win!");
            Application.Quit();
        }
    }
}
