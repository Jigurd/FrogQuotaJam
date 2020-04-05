using UnityEngine;

public class OfficeWindow : MonoBehaviour
{
    private void OnMouseDown()
    {
        GameState.GameMode = GameMode.City;
    }
}
