using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject gameUI;
    public GameObject pauseMenu; 
    public static bool isPaused;

    public GameObject Camera; 

    // public Behaviour Gun; 

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false); 
        gameUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        // When Pause Key is Pressed, Game is Paused
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Pause key is Pressed");

            if(isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true); 

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
        
        // While Game is Paused, Turn off Gun Script
        // Gun.enabled = false;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        
        // While Game is Resumed, Turn on Crosshair
        gameUI.SetActive(true);

        // Resume Game 
        Time.timeScale = 1f; 
        isPaused = false; 

        // While Game is Resumed, make Cursor Invisible and Locked 
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // While Game is Resumed, turn on Standard Cam
        // Camera.SetActive(true);
        
        // While Game is Resumed, turn on Gun Script 
        // Gun.enabled = true;
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
