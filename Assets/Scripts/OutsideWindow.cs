using UnityEngine;

public class OutsideWindow : MonoBehaviour
{
    [SerializeField] private GameObject _player = null;
    [Tooltip("Basically how many seconds after entering city mode before player can interact with window.")]
    [SerializeField] private float _officeToCityTransitionTime = 4.0f;
    private float _timeSinceOfficeToCityTransition = 0.0f;

    private void Awake()
    {
        GameState.OnGameModeSet[GameMode.City] += OnCityGameModeSet;
        GameState.OnGameModeSet[GameMode.Office] += OnOfficeGameModeSet;
    }

    private void OnDestroy()
    {
        GameState.OnGameModeSet[GameMode.City] -= OnCityGameModeSet;
        GameState.OnGameModeSet[GameMode.Office] += OnOfficeGameModeSet;
    }

    private void Update()
    {
        if (GameState.GameMode == GameMode.City)
        {
            _timeSinceOfficeToCityTransition += Time.deltaTime;
        }
    }

    private void OnCityGameModeSet()
    {
        _player.SetActive(true);
        _player.transform.position = transform.position;
        _timeSinceOfficeToCityTransition = 0.0f;
    }

    private void OnOfficeGameModeSet()
    {
        if (_player==null)
        {
            _player = FindObjectOfType<PlayerController>().gameObject;
        }
        _player.SetActive(false);
        _player.transform.position = transform.position;
    }

    private void OnMouseDown()
    {
        GameState.GameMode = GameMode.City;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_timeSinceOfficeToCityTransition > _officeToCityTransitionTime)
        {
            if (collision.gameObject.GetComponent<PlayerController>() != null)
            {
                GameState.GameMode = GameMode.Office;
            }
        }
    }
}
