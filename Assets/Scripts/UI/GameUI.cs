using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public ScoreSystem score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI arenaText;

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score.currentScore.ToString();
        arenaText.text = "Arenas Visited: " + score.arenasVisited.ToString();
    }
}
