using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
public class Paper : MonoBehaviour
{
    private Camera _camera;
    private Vector3 _startDragMousePosition;
    private Vector3 _startDragPosition;
    private SpriteRenderer _spriteRenderer;
    private bool _dragging = false;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _camera = GameObject.Find("OfficeCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_dragging)
        {
            Vector2 position = _startDragPosition + _camera.ScreenToWorldPoint(Input.mousePosition) - _startDragMousePosition;
            transform.position = new Vector3(
                position.x,
                position.y,
                transform.position.z
            );
        }
    }

    private void OnMouseDown()
    {
        _spriteRenderer.sortingOrder += 1;
        _dragging = true;
        _startDragMousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        _startDragPosition = transform.position;
    }
    private void OnMouseUp()
    {
        _dragging = false;
    }
}
