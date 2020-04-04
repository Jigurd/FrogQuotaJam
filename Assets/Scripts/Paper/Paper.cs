using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
public class Paper : MonoBehaviour
{
    public static List<Paper> Papers { get; set; } = new List<Paper>();    

    private Camera _camera;
    private Vector3 _startDragMousePosition;
    private Vector3 _startDragPosition;
    private bool _dragged = false;

    public SpriteRenderer SpriteRenderer { get; set; }

    private void Awake()
    {
        Papers.Add(this);
        SpriteRenderer = GetComponent<SpriteRenderer>();
        _camera = GameObject.Find("OfficeCamera").GetComponent<Camera>();
        SortPapers();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0)) _dragged = false;
        if (_dragged)
        {
            Vector2 position = _startDragPosition + _camera.ScreenToWorldPoint(Input.mousePosition) - _startDragMousePosition;
            transform.position = new Vector3(
                position.x,
                position.y,
                transform.position.z
            );
        }
    }

    public void StartDrag()
    {
        Papers.Remove(this);
        Papers.Add(this);
        SortPapers();
        _dragged = true;
        _startDragMousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        _startDragPosition = transform.position;
    }

    public static void SortPapers()
    {
        for (int i = 0; i < Papers.Count; i++)
        {
            Papers[i].SpriteRenderer.sortingOrder = i;
        }
    }
}
