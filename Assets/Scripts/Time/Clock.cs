using UnityEngine;

public class Clock : MonoBehaviour
{
    [SerializeField] private Transform _minutePivot = null;
    [SerializeField] private Transform _hourPivot = null;

    private void Update()
    {
        RotateHands();
    }

    public void RotateHands()
    {
        float minute = InGameTimeManager.Minute + InGameTimeManager.Second / 60.0f;
        _minutePivot.localEulerAngles = new Vector3(
            _minutePivot.localEulerAngles.x,
            _minutePivot.localEulerAngles.y,
            -minute / 60.0f * 360.0f
        );
        _hourPivot.localEulerAngles = new Vector3(
            _hourPivot.localEulerAngles.x,
            _hourPivot.localEulerAngles.y,
            -(InGameTimeManager.Hour + minute / 60.0f) / 12.0f * 360.0f
        );
    }
}
