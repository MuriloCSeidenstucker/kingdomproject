using UnityEngine;

public class Coin : MonoBehaviour, IPooledObject
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _fullCoin;
    [Space]
    [Range(1, 4, order = 1)]
    [SerializeField] private int _bouncinessLimit = 3;

    private GameHandler _gameHandler;
    private Vector2 _gravity = new Vector2(0f, -9.81f);
    private Vector2 _ground = new Vector2(0f, -2.9f);
    private Vector2 _currentVelocity;
    private Vector3 _coinCollectorPos;
    private float _bounciness;
    private float _attractSpeed;
    private float _scaleSpeed;
    private float _finalScaleMultiplier;
    private bool _naturalMovementEnded;
    private bool _activatedBehavior;

    public bool NaturalMovementEnded { get { return _naturalMovementEnded; } }

    private void Awake()
    {
        _gameHandler = GetComponentInParent<GameHandler>();
    }

    private void Update()
    {
        PerformCoinBehavior();
    }

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
        _currentVelocity = -velocityAfterCollision;

        // To prevent the coin from sticking to the ground.
        // TODO: Check another way to handle it.
        previousPos.y = _ground.y + 0.1f;
    }

    private void PerformCoinBehavior()
    {
        if (!_activatedBehavior) return;

        _animator.enabled = false;
        _spriteRenderer.sprite = _fullCoin;

        Vector3 startPos = transform.position;
        Vector3 endPos = _coinCollectorPos;
        Vector3 startScale = transform.localScale;
        Vector3 endScale = Vector3.one * _finalScaleMultiplier;

        if (startPos != endPos)
        {
            transform.position = Vector3.MoveTowards(startPos, endPos, _attractSpeed * Time.deltaTime);
        }
        else if (startScale != endScale)
        {
            transform.localScale = Vector3.MoveTowards(startScale, endScale, _scaleSpeed * Time.deltaTime);
        }
        else
        {
            // TODO
            _gameHandler._coinPool.ReturnToPool(this);
        }
    }

    private void ActivateCoinBehavior(in CoinCollectorData data)
    {
        _attractSpeed = data.AttractSpeed;
        _scaleSpeed = data.ScaleSpeed;
        _finalScaleMultiplier = data.FinalScaleMultiplier;
        _coinCollectorPos = data.CoinCollector.position;
        _activatedBehavior = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        ICoinCollector collector = other.GetComponent<ICoinCollector>();
        if (collector != null && NaturalMovementEnded)
        {
            ActivateCoinBehavior(collector.ReactToCoinCollision());
        }
    }

    private void ResetCoinSetup()
    {
        _naturalMovementEnded = false;
        _activatedBehavior = false;
        _bouncinessLimit = 3;
        transform.localScale = Vector3.one;
        _animator.enabled = true;
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
