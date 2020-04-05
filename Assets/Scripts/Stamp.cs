using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        if (GameState.IsPaused) return;

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
                var paper = DragHandler.GetTopPaperUnderMouse();
                if (paper != null)
                {
                    // check that the paper is also under the stamp
                    var ray = new Ray(transform.position + Vector3.forward, Vector3.back);
                    var hits = Physics2D.GetRayIntersectionAll(ray);
                    if (hits.AsEnumerable().Select(i => i.collider.GetComponent<Paper>()).Where(i => i == paper).Count() > 0)
                    {
                        var stampMark = Instantiate(
                            _stampMarkPrefab,
                            transform.position,
                            Quaternion.identity,
                            null);
                        stampMark.transform.SetParent(paper.transform);
                        paper.Contents.Add(stampMark.GetComponent<SpriteRenderer>());
                        Paper.SortPapers();
                    }
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
            if (Stamps[i] == null)
            {
                Stamps.Remove(Stamps[i]);
                continue;
            }
            Stamps[i].SpriteRenderer.sortingOrder = i;
        }
    }
}
