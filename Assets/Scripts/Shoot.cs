using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float forceBuild = 20f;
    [SerializeField] private float maximumHoldTime = 5f;
    [SerializeField] private float lineSpeed = 10f;
    private LineRenderer _line;
    private bool _lineActive = false;

    private float _pressTimer = 0f;
    private float _launchForce = 0f;
    private void Start()
    {
    
        _line = GetComponent<LineRenderer>();
        _line.SetPosition(1,Vector3.zero);
        _line.sortingLayerName = "Main Sprites";
    }

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

        if (Input.GetMouseButtonDown(0))
        {
            _pressTimer = 0f;
            _lineActive = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _lineActive = false;
            _line.SetPosition(1, Vector3.zero);
        }

        if (_lineActive)
        {
            _line.SetPosition(1, Vector3.right * _pressTimer * lineSpeed);
        }
    }
}