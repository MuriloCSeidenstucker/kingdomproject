using UnityEngine;

public class Coin : MonoBehaviour, IPooledObject
{
    [SerializeField] private Animator _animator;
    [Space]
    [Range(1, 4, order = 1)]
    [SerializeField] private int _bouncinessLimit = 2;

    private Vector2 _gravity = new Vector2(0f, -9.81f);
    private Vector2 _ground = new Vector2(0f, -2.9f);
    private Vector2 _currentVelocity;
    private float _bounciness;
    private bool _naturalMovementEnded;
    private bool _activatedBehavior;
    private bool _wasCollected;

    public Animator Animator { get { return _animator; } }
    public bool NaturalMovementEnded { get { return _naturalMovementEnded; } }
    public bool ActivatedBehavior { get { return _activatedBehavior; } set { _activatedBehavior = value; } }
    public bool WasCollected { get { return _wasCollected; } set { _wasCollected = value; } }

    private void FixedUpdate()
    {
        if (!_naturalMovementEnded)
            ProcessNaturalMovement();
    }

    private void ProcessNaturalMovement()
    {
        _currentVelocity += _gravity * Time.fixedDeltaTime;

        Vector2 previousPosition = transform.position;
        if (previousPosition.y <= _ground.y)
        {
            ApllyBounciness(ref previousPosition);
        }
        Vector2 currentPosition = previousPosition + _currentVelocity * Time.fixedDeltaTime;

        if (_bouncinessLimit > 0)
        {
            transform.position = currentPosition;
        }
        else
        {
            _currentVelocity = Vector2.zero;
            _naturalMovementEnded = true;
        }
    }

    private void ApllyBounciness(ref Vector2 previousPos)
    {
        _bouncinessLimit--;
        _bounciness = Random.Range(0.1f, 0.6f);

        Vector2 velocityAfterCollision = _currentVelocity * _bounciness;
        _currentVelocity.y = -velocityAfterCollision.y;

        // To prevent the coin from sticking to the ground.
        // TODO: Check another way to handle it.
        previousPos.y = _ground.y + 0.1f;
    }

    private void ResetCoinSetup()
    {
        _naturalMovementEnded = false;
        ActivatedBehavior = false;
        WasCollected = false;
        _animator.enabled = true;
        _bouncinessLimit = 2;
        transform.localScale = Vector3.one;
    }

    public void ApllyVelocityAfterEjection(in float ejectionForce)
    {
        float randomX = Random.Range(-1f, 1f);
        Vector2 velocity = new Vector2(randomX, ejectionForce);
        _currentVelocity += velocity;
    }

    public void OnInstantiated()
    {
        ResetCoinSetup();
    }

    public void OnEnabledFromPool()
    {
        ResetCoinSetup();
    }

    public void OnDisabledFromPool()
    {
        ResetCoinSetup();
    }
}
