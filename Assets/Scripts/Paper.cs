using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
public class Paper : MonoBehaviour
{
    private Camera _camera;
    private Vector2 _startDragPosition;
    private bool _dragging = false;

    private void Awake()
    {
        _camera = GameObject.Find("OfficeCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_dragging)
        {
            Vector2 delta = _camera.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(
                delta.x,
                delta.y,
                transform.position.z
            );
        }
    }

    private void OnMouseDown()
    {
        _dragging = true;
        _startDragPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
    }
    private void OnMouseUp()
    {
        _dragging = false;
    }
}
