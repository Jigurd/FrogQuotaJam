using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Paper : MonoBehaviour
{
    public static List<Paper> Papers { get; set; } = new List<Paper>();    

    private Camera _camera;
    private Vector3 _startDragMousePosition;
    private Vector3 _startDragPosition;
    private bool _dragged = false;

    public SpriteRenderer SpriteRenderer { get; set; }
    public List<SpriteRenderer> Contents { get; set; } = new List<SpriteRenderer>();

    private void Awake()
    {
        Papers.Add(this);
        SpriteRenderer = GetComponent<SpriteRenderer>();
        _camera = GameObject.Find("OfficeCamera").GetComponent<Camera>();
        SortPapers();
        SpriteRenderer.color = new Color(Random.Range(0.0f, 0.1f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
    }

    // Update is called once per frame
    void Update()
    {
        if (GameState.IsPaused) return;

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
        int sortingOrder = 0;
        for (int i = 0; i < Papers.Count; i++)
        {
            if (Papers[i] == null)
            {
                Papers.RemoveAt(i);
                continue;
            }
            Papers[i].SpriteRenderer.sortingOrder = sortingOrder++;
            foreach (var content in Papers[i].Contents)
            {
                content.sortingOrder = sortingOrder++;
            }
        }
    }
}
