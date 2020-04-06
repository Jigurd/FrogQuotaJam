using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    public static ScoreTracker Instance;
    public int CurrentScore = 0;

    private void Awake()
    {
        Instance = this;

        DontDestroyOnLoad(this);
    }

    public void UpdateCurrentScore(int addScore)
    {
        CurrentScore += addScore;
    }
}
