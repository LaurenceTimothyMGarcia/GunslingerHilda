using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public GameObject gameUI;
    public GameObject deathMenu;
    public static bool isPaused;

    // public GameObject Camera;

    public GameObject player;

    void Start()
    {
        deathMenu.SetActive(false); 
        gameUI.SetActive(true);
    }

    void Update()
    {
        
    }

    public void DeathScreen()
    {
        deathMenu.SetActive(true); 

        // While Game is Paused, Turn off Crosshair 
        gameUI.SetActive(false); 

        // Pause Game 
        Time.timeScale = 0f; 
        isPaused = true; 

        // While Game is Paused, make Cursor visible and unlocked 
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // While Game is Paused, Turn off Standard Cam 
        // Camera.SetActive(false); 
    }

    public void GoToMainMenu()
    {
        // Go To Main Menu Scene 
        Time.timeScale = 1f;
        SceneManager.LoadScene("TitleScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
