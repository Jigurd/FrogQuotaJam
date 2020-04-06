using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DayTransition : MonoBehaviour
{
    [SerializeField] private Text _text = null;
    [SerializeField] private Button _continueButton = null;
    [SerializeField] private Button _quitButton = null;

    private void Awake()
    {
        _continueButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("MainScene");
        });
        _quitButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("HighscoreScene");
        });
        _text.text =
            "day " + (InGameTimeManager.Day - 1) + " done\n" +
            "day " + InGameTimeManager.Day + " is not available due to technical issues :(\n" +
            "please retire.";
        _continueButton.gameObject.SetActive(false);
    }
}
