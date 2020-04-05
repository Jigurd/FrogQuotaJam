using System.Linq;
using UnityEngine;

public class DragHandler : MonoBehaviour
{
    private static Camera _camera;

    private void Awake()
    {
        if (_camera == null) _camera = UnityEngine.GameObject.Find("OfficeCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        if (GameState.IsPaused) return;

        if (Input.GetMouseButtonDown(0))
        {
            var closestStamp = GetTopStampUnderMouse();
            if (closestStamp != null)
            {
                closestStamp.StartDrag();
                return;
            }
            var closestPaper = GetTopPaperUnderMouse();
            if (closestPaper != null)
            {
                closestPaper.StartDrag();
                return;
            }
        }
    }

    public static Stamp GetTopStampUnderMouse()
    {
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        var hits = Physics2D.GetRayIntersectionAll(ray);
        return hits.AsEnumerable()
            .Select(hit => hit.collider.gameObject.GetComponent<Stamp>())
            .Where(stamp => stamp != null)
            .OrderByDescending(stamp => stamp.SpriteRenderer.sortingOrder)
            .FirstOrDefault();
    }
    public static Paper GetTopPaperUnderMouse()
    {
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        var hits = Physics2D.GetRayIntersectionAll(ray);
        return hits.AsEnumerable()
            .Select(hit => hit.collider.gameObject.GetComponent<Paper>())
            .Where(paper => paper != null)
            .OrderByDescending(paper => paper.SpriteRenderer.sortingOrder)
            .FirstOrDefault();
    }
}
