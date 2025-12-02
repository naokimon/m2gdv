using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float forceBuild = 20f;
    [SerializeField] private float maximumHoldTime = 5f;

    private float _pressTimer = 0f;
    private float _launchForce = 0f;

    private void Update()
    {
        HandleShot();
    }

    private void HandleShot()
    {
        if (Mouse.current == null) return;

        // Mouse pressed
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            _pressTimer = 0f;
        }

        // Mouse released
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            _launchForce = _pressTimer * forceBuild;

            GameObject ball = Instantiate(prefab, transform.parent);

            ball.transform.rotation = transform.rotation;
            ball.transform.position = transform.position;

            ball.GetComponent<Rigidbody2D>()
                .AddForce(ball.transform.right * _launchForce, ForceMode2D.Impulse);
        }

        // Build force timer
        if (_pressTimer < maximumHoldTime && Mouse.current.leftButton.isPressed)
        {
            _pressTimer += Time.deltaTime;
        }
    }
}
