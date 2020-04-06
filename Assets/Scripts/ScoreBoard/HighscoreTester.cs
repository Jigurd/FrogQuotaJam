using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreTester : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            string username = "";
            int score = 0;
            for (int i = 0; i < Random.Range(3, 11); i++)
            {
                // RandomXDize username
                char randomChar = (char)('a' + Random.Range(0, 26));
                username += randomChar;

                // RandomXDize score
                score = Random.Range(1, 100000);
                Highscores.instance.UpdateCurrentScore(100);
            }


            //Highscores.AddNewHighScore(username, score);
        }
    }
}
