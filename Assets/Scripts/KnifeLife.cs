using UnityEngine;

public class KnifeLife : MonoBehaviour
{
    private Shoot _shoot;
    private Rigidbody2D _rb;

    [Header("Stuck Detection")]
    [SerializeField] private float stopVelocityThreshold = 0.05f;
    [SerializeField] private float timeBeforeDestroy = 2f;

    private float _stillTimer = 0f;
    private bool _killed = false;

    public void Init(Shoot shoot)
    {
        _shoot = shoot;
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_killed || _rb == null) return;

        // Check if knife is almost not moving
        if (_rb.linearVelocity.magnitude <= stopVelocityThreshold)
        {
            _stillTimer += Time.deltaTime;

            if (_stillTimer >= timeBeforeDestroy)
            {
                Kill();
            }
        }
        else
        {
            // Knife moved again → reset timer
            _stillTimer = 0f;
        }
    }

    // Call this when the knife reaches the killzone OR stops moving
    public void Kill()
    {
        if (_killed) return;
        _killed = true;

        _shoot?.OnKnifeDestroyed();
        Destroy(gameObject);
    }
}
