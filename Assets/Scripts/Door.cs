using UnityEngine;

public class Door : MonoBehaviour
{
    public bool IsOpen { get; set; } = false;

    private void Update()
    {
        if (GameState.IsPaused) return;

        if (Input.GetKeyDown(KeyCode.O)) IsOpen = !IsOpen;
        float x = transform.localScale.x;
        if (IsOpen)
        {
            x = Mathf.Lerp(x, 0.0f, Time.deltaTime * 20.0f);
        }
        else
        {
            x = Mathf.Lerp(x, 1.0f, Time.deltaTime * 20.0f);
        }
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }
}
