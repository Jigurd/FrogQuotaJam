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
            Application.Quit();
        });
        _text.text =
            "day " + (InGameTimeManager.Day - 1) + " = done\n" +
            "day " + InGameTimeManager.Day + " = begin??? :^)\n" +
            "summary:\nsomething something\n" +
            "you did [bad|good] 42069\n" +
            "no raise\n" +
            "no promotion\n" +
            "no suspicion\n" +
            "wholesome 100 big chungus";
    }
}
