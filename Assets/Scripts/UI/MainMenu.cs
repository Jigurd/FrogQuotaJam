using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _playButton = null;
    [SerializeField] private Button _quitButton = null;

    private void Awake()
    {
        _playButton.onClick.AddListener(() =>
        {
            Debug.Log("Play");
            SceneManager.LoadScene("OfficeScene");
        });
        _quitButton.onClick.AddListener(() =>
        {
            Debug.Log("Quit");
            Application.Quit();
        });

        SoundManager.PlayMusic("TestMusic");
    }
}
