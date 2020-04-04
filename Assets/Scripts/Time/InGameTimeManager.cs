using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameTimeManager : MonoBehaviour
{
    [SerializeField] private float _inGameSecondsPerRealLifeSeconds = 300.0f;
    public static int Day { get; set; } = 1;
    public static int Hour { get; set; } = 9;
    public static int Minute { get; set; } = 0;
    public static float Second { get; set; } = 0;

    public static Action<int> OnMinute { get; set; }

    private void Update()
    {
        Second += Time.deltaTime * _inGameSecondsPerRealLifeSeconds;
        if (Second >= 60)
        {
            Second = 0;
            Minute++;
            OnMinute?.Invoke(Minute);
            if (Minute > 59)
            {
                Minute = 0;
                Hour++;
                Debug.Log("Hours: " + Hour);
                if (Hour > 16)
                {
                    Hour = 9;
                    Day++;
                    Debug.Log("Day: " + Day);
                    SceneManager.LoadScene("DayTransition");
                }
            }
        }
    }
}
