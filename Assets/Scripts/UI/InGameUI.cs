using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private UnityEngine.GameObject _pauseMenu = null;
    [SerializeField] private Button _continueButton = null;
    [SerializeField] private Button _mainMenuButton = null;
    [SerializeField] private Button _quitButton = null;

    private void Awake()
    {
        _pauseMenu.SetActive(false);
        _continueButton.onClick.AddListener(() =>
        {
            _pauseMenu.SetActive(false);
            // unpause
            GameState.IsPaused = false;
        });
        _mainMenuButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("MainMenu");
        });
        _quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _pauseMenu.SetActive(!_pauseMenu.activeSelf);
            // pause if menu active
            GameState.IsPaused = _pauseMenu.activeSelf;
        }
    }
}
