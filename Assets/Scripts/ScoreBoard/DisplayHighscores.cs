using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHighscores : MonoBehaviour
{
    [SerializeField] private float _highscoreRefreshRate = 30;
    [SerializeField] private Text[] _highscoreText = null;
    [SerializeField] private Text _lastScore = null;

    public static Highscores HighscoreManager;

    private void Awake()
    {
        HighscoreManager = GetComponent<Highscores>();

        _lastScore.text = "";

        for (int i = 0; i < _highscoreText.Length; i++)
        {
            _highscoreText[i].text = i + 1 + ". Fetching...";
        }

        StartCoroutine(RefreshHighscores());
    }

    public void OnHighscoresDownloaded(Highscore[] highscoreList)
    {
        for (int i = 0; i < _highscoreText.Length; i++)
        {
            _highscoreText[i].text = i + 1 + ". Fetching...";
            if (highscoreList.Length > i)
            {
                _highscoreText[i].text = i + 1 + ". ";
                _highscoreText[i].text += highscoreList[i].Username + " - " + highscoreList[i].Score;
            }
        }
    }
    IEnumerator RefreshHighscores()
    {
        while (true)
        {
            //print("Meme");
            HighscoreManager.DownloadHighscores();
            yield return new WaitForSeconds(_highscoreRefreshRate);
        }
    }
    public void ShowYourLatestScore(string username, int score)
    {
        _lastScore.text ="Your score: " + username + " - " + score.ToString();
    }
}
