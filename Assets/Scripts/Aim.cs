using UnityEngine;
using UnityEngine.InputSystem;

public class Aim : MonoBehaviour
{
    void Update()
    {
        // Get mouse position from new Input System
        Vector2 mousePos = Mouse.current.position.ReadValue();

        Vector3 worldPos = transform.position;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);

        Vector3 dir = (Vector3)mousePos - screenPos;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}