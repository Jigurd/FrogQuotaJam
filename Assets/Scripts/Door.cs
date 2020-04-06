using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Sprite _openSprite = null;
    [SerializeField] private Sprite _closedSprite = null;

    private SpriteRenderer _spriteRenderer = null;

    public bool IsOpen { get; set; } = false;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (GameState.IsPaused) return;
        _spriteRenderer.sprite = IsOpen ? _openSprite : _closedSprite;
    }
}
