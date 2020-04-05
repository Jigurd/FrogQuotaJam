using UnityEngine;

public class OfficeLeftWall : MonoBehaviour
{
    [SerializeField] private Camera _officeCamera = null;

    private void Update()
    {
        transform.position = new Vector3(
            _officeCamera.ScreenToWorldPoint(Vector3.right * _officeCamera.rect.x * _officeCamera.scaledPixelWidth).x,
            transform.position.y,
            0.0f
        );
    }
}
