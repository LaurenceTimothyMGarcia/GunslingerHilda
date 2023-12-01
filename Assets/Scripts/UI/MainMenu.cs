using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("PrimaryLevel"); // The scene here is a place holder, substitute LarryRoom for the scene with the first level
                                             // this should work once scene build order is decided
    }

    public void LoadSettingsMenu()
    {
        SceneManager.LoadScene("SettingsMenuSceneHere"); // The scene here is a place holder, substitute this for the scene with the settings menu
                                                         // this should work once scene build order is decided
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
