using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[DisallowMultipleComponent]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] protected float _walkSpeed = 2.0f;
    [SerializeField] protected float _runSpeed = 4.0f;
    [SerializeField] protected float _movementAcc = 100.0f;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private Vector2 _currentVelocity;

    public Vector2 CurrentVelocity { get { return _currentVelocity; } }
    public float WalkSpeed { get { return _walkSpeed; } }
    public bool IsFacingRight => _spriteRenderer.flipX == false;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    protected virtual void Update()
    {
        FlipSprite();
    }

    private void FixedUpdate()
    {
        Vector2 previousPosition = _rigidbody.position;
        Vector2 currentPosition = previousPosition + _currentVelocity * Time.fixedDeltaTime;
        _rigidbody.MovePosition(currentPosition);
    }

    private void FlipSprite()
    {
        //TODO: Verificar se é possível reduzir os if.
        if (_spriteRenderer != null)
        {
            if (CurrentVelocity.x > 0)
            {
                _spriteRenderer.flipX = false;
            }
            else if (CurrentVelocity.x < 0)
            {
                _spriteRenderer.flipX = true;
            }
        }
    }

    protected virtual float SpeedHandler(in bool runInput)
    {
        return runInput ? _runSpeed : _walkSpeed;
    }

    public void ProcessMovementInput(in Vector2 movementInput, in bool runInput)
    {
        float currentSpeed = SpeedHandler(in runInput);
        float desiredHorizontalSpeed = movementInput.x * currentSpeed;

        _currentVelocity.x = Mathf.MoveTowards(_currentVelocity.x, desiredHorizontalSpeed, _movementAcc * Time.deltaTime);
    }
}
