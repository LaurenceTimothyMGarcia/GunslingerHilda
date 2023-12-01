using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScoreSystem")]
public class ScoreSystem : ScriptableObject
{
    public float currentScore;

    public int enemiesKilled = 0;
    public int arenasVisited = 1;

    public void AddScore()
    {
        currentScore = enemiesKilled / arenasVisited;
    }

    public void EnemyKilled(int scorePoints)
    {
        enemiesKilled += scorePoints;
        AddScore();
    }

    public void arenaVisited()
    {
        arenasVisited += 1;
        AddScore();
    }

    public void ResetScore()
    {
        currentScore = 0;
        enemiesKilled = 0;
        arenasVisited = 1;
    }
}
