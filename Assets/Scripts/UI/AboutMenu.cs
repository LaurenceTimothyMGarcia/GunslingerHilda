using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class AboutMenu : MonoBehaviour
{
    public GameObject aboutGame;
    public GameObject howToPlay;
    public GameObject credits;

    // Start is called before the first frame update
    void Start()
    {
        aboutGame.SetActive(true);
        howToPlay.SetActive(false);
        credits.SetActive(false);
    }

    public void LoadAboutGame()
    {
        aboutGame.SetActive(true);
        howToPlay.SetActive(false);
        credits.SetActive(false);
    }

    public void LoadHowToPlay()
    {
        aboutGame.SetActive(false);
        howToPlay.SetActive(true);
        credits.SetActive(false);
    }

    public void LoadCredits()
    {
        aboutGame.SetActive(false);
        howToPlay.SetActive(false);
        credits.SetActive(true);
    }
}
