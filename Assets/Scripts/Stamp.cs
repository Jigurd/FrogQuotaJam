using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamp : MonoBehaviour
{
    public static List<Stamp> Stamps { get; set; } = new List<Stamp>();

    [SerializeField] private GameObject _stampMarkPrefab = null;

    private Camera _camera;
    private Vector3 _startDragMousePosition;
    private Vector3 _startDragPosition;
    private bool _dragged = false;

    public SpriteRenderer SpriteRenderer { get; set; }

    private void Awake()
    {
        Stamps.Add(this);
        SpriteRenderer = GetComponent<SpriteRenderer>();
        _camera = GameObject.Find("OfficeCamera").GetComponent<Camera>();
        SortStamps();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0)) _dragged = false;
        if (_dragged)
        {
            Vector2 delta = _camera.ScreenToWorldPoint(Input.mousePosition) - _startDragMousePosition;
            Vector2 position = _startDragPosition + new Vector3(delta.x, delta.y);
            transform.position = new Vector3(
                position.x,
                position.y,
                transform.position.z
            );

            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log(DragHandler.GetTopPaperUnderMouse());
                var paper = DragHandler.GetTopPaperUnderMouse();
                if (paper != null)
                {
                    var sr = Instantiate(
                        _stampMarkPrefab,
                        transform.position,
                        Quaternion.identity,
                        null);
                    sr.transform.SetParent(paper.transform);
                    paper.Contents.Add(sr.GetComponent<SpriteRenderer>());
                    Paper.SortPapers();
                }
            }
        }
    }

    public void StartDrag()
    {
        Stamps.Remove(this);
        Stamps.Add(this);
        SortStamps();
        _dragged = true;
        _startDragMousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        _startDragPosition = transform.position;
    }

    public static void SortStamps()
    {
        for (int i = 0; i < Stamps.Count; i++)
        {
            Stamps[i].SpriteRenderer.sortingOrder = i;
        }
    }
}
