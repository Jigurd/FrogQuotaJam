using UnityEngine;

public class CameraResizer : MonoBehaviour
{
    [SerializeField] private Camera _cityCamera = null;
    [SerializeField] private Camera _officeCamera = null;

    private void Awake()
    {
        GameState.OnGameModeSet[GameMode.Office] += OnOfficeGameModeSet;
        GameState.OnGameModeSet[GameMode.City] += OnCityGameModeSet;
    }

    private void OnOfficeGameModeSet()
    {
        _cityCamera.rect = new Rect(0.0f, 0.0f, 0.39f, 1.0f);
        _officeCamera.rect = new Rect(0.41f, 0.0f, 0.59f, 1.0f);
    }

    private void OnCityGameModeSet()
    {
        _cityCamera.rect = new Rect(0.0f, 0.0f, 0.59f, 1.0f);
        _officeCamera.rect = new Rect(0.61f, 0.0f, 0.49f, 1.0f);
    }
}
