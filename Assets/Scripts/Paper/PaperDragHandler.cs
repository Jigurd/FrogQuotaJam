using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PaperDragHandler : MonoBehaviour
{
    private Camera _camera;

    private void Awake()
    {
        _camera = GameObject.Find("OfficeCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            var hits = Physics2D.GetRayIntersectionAll(ray);
            var closestPaper = hits.AsEnumerable()
                .Select(hit => hit.collider.gameObject.GetComponent<Paper>())
                .Where(paper => paper != null)
                .OrderByDescending(paper => paper.SpriteRenderer.sortingOrder)
                .FirstOrDefault();
            closestPaper.StartDrag();
        }
    }
}
