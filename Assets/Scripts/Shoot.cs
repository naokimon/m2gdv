using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float forceBuild = 20f;
    [SerializeField] private float maximumHoldTime = 5f;
    [SerializeField] private float lineSpeed = 10f;

    [Header("Knife Limit")]
    [SerializeField] public int maxKnives = 7;

    public int knivesUsed { get; private set; } = 0;

    private LineRenderer _line;
    private bool _lineActive = false;

    private float _pressTimer = 0f;
    private float _launchForce = 0f;

    private bool _canShoot = true;
    public GameObject _currentKnife;

    public System.Action OnKnifeDestroyedEvent;

    public bool HasKnivesLeft => knivesUsed < maxKnives;
    public bool HasActiveKnife => _currentKnife != null;

    private void Start()
    {
        _line = GetComponent<LineRenderer>();
        _line.positionCount = 2;
        _line.SetPosition(1, Vector3.zero);
        _line.sortingLayerName = "Line";
    }

    private void Update()
    {
        HandleShot();
    }

    private void HandleShot()
    {
        if (!_canShoot) return;
        if (knivesUsed >= maxKnives) return;
        if (Mouse.current == null) return;

        // Mouse pressed
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            _pressTimer = 0f;
            _lineActive = true;
        }

        // Build force timer
        if (Mouse.current.leftButton.isPressed && _pressTimer < maximumHoldTime)
        {
            _pressTimer += Time.deltaTime;
        }

        // Mouse released
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            _lineActive = false;
            _line.SetPosition(1, Vector3.zero);

            ShootKnife();
        }

        if (_lineActive)
        {
            _line.SetPosition(1, Vector3.right * _pressTimer * lineSpeed);
        }
    }

    private void ShootKnife()
    {
        if (knivesUsed >= maxKnives) return;

        _launchForce = _pressTimer * forceBuild;

        _currentKnife = Instantiate(prefab, transform.position, transform.rotation, transform.parent);

        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.currentKnife = _currentKnife;
        }

        Rigidbody2D rb = _currentKnife.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(_currentKnife.transform.right * _launchForce, ForceMode2D.Impulse);
        }

        _currentKnife.GetComponent<KnifeLife>()?.Init(this);

        knivesUsed++;
        _canShoot = false;
    }

    public void OnKnifeDestroyed()
    {
        _canShoot = true;
        _currentKnife = null;

        OnKnifeDestroyedEvent?.Invoke();
    }

}
