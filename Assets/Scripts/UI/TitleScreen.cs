using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject aboutMenu;

    public void Start()
    {
        mainMenu.SetActive(true);
        aboutMenu.SetActive(false);
    }

    public void AboutMenu()
    {
        mainMenu.SetActive(false);
        aboutMenu.SetActive(true);
    }

    public void MainMenu()
    {
        mainMenu.SetActive(true);
        aboutMenu.SetActive(false);
    }
}
