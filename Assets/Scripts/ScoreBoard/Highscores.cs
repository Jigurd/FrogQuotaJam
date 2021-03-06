﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Highscores : MonoBehaviour
{
    const string _privateCode = "NqgidQ3rJUq9QNmo_Jq_LgdnKGtE7XwEOq5ASL_Mq5YA";
    const string _publicCode = "5e8a4f0b403c2d12b8bd0d97";
    const string _webURL = "http://dreamlo.com/lb/";

    public static Highscores Instance;
    public Highscore[] HighscoresList;

    private DisplayHighscores _highscoresDisplay;
    
    [SerializeField] private string _username = "";
    [SerializeField] private GameObject _usernameSubmitObject = null;
    [SerializeField] private Text _usernameChosenByPlayer = null;
    [SerializeField] private Button _mainMenuButton = null;

    private void Awake()
    {
        Instance = this;
        _highscoresDisplay = GetComponent<DisplayHighscores>();

        //AddNewHighScore("lamo", 420);
        //AddNewHighScore("xd", 69);
        //DownloadHighscores();

        _mainMenuButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("MainMenu");
        });
    }
    public void SetUsername()
    {
        _username = _usernameChosenByPlayer.text.ToString();

        // Maybe do this differently
        AddNewHighScore(_username, ScoreTracker.Instance.CurrentScore);
        _usernameSubmitObject.SetActive(false);
    }
    public static void AddNewHighScore(string username, int score)
    {
        Instance._highscoresDisplay.ShowYourLatestScore(username, score);
        Instance.StartCoroutine(Instance.UploadNewHighscore(username, score));
    }
    IEnumerator UploadNewHighscore(string username, int score)
    {
        UnityWebRequest www = new UnityWebRequest(_webURL + _privateCode + "/add/" + UnityWebRequest.EscapeURL(username) + "/" + score);
        //www.downloadHandler = new DownloadHandlerBuffer();
        yield return www.SendWebRequest();

        if (string.IsNullOrEmpty(www.error))
        {
            print("Upload Successful");
            DownloadHighscores();
        }
        else
        {
            print("Error uploading: " + www.error);
        }
    }
    public void DownloadHighscores()
    {
        StartCoroutine(DownloadHighscoresFromDatabase());
    }
    IEnumerator DownloadHighscoresFromDatabase()
    {
        UnityWebRequest www = new UnityWebRequest(_webURL + _publicCode + "/pipe/");
        www.downloadHandler = new DownloadHandlerBuffer();
        yield return www.SendWebRequest();

        if (string.IsNullOrEmpty(www.error))
        {
            //print(www.downloadHandler.text);
            FormatHighscores(www.downloadHandler.text);
            _highscoresDisplay.OnHighscoresDownloaded(HighscoresList);
        }
        else
        {
            print("Error downloading: " + www.error);
        }
    }
    private void FormatHighscores(string textStream)
    {
        string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        HighscoresList = new Highscore[entries.Length];

        for (int i = 0; i < entries.Length; i++)
        {
            string[] entryInfo = entries[i].Split(new char[] { '|' });
            string username = entryInfo[0];
            int score = int.Parse(entryInfo[1]);
            HighscoresList[i] = new Highscore(username, score);
            //print(highscoresList[i].Username + ": " + highscoresList[i].Score);
        }
    }
}
public struct Highscore
{
    public string Username;
    public int Score;

    public Highscore(string username, int score)
    {
        Username = username;
        Score = score;
    }
}
