using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[DisallowMultipleComponent]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float _walkSpeed = 10.0f;
    [SerializeField] private float _runSpeed = 10.0f;
    [SerializeField] private float _movementAcc = 100.0f;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private Vector2 _currentVelocity;

    public Vector2 CurrentVelocity { get { return _currentVelocity; } }
    public float WalkSpeed { get { return _walkSpeed; } }
    public bool IsFacingRight => _spriteRenderer.flipX == false;

    //TODO: Específico do player.
    [Space]
    [Header("Player specific parameters")]
    [SerializeField] private Transform _breathlessVFX;
    [SerializeField] private Transform _muzzle;
    [SerializeField] private float _muzzleOffsetX = 0.4f;
    private PlayerStamina _playerStamina;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _playerStamina = GetComponent<PlayerStamina>();
    }

    private void Update()
    {
        FlipSprite();
        UpdateBreathlessVFXSettings();
    }

    private void FixedUpdate()
    {
        Vector2 previousPosition = _rigidbody.position;
        Vector2 currentPosition = previousPosition + _currentVelocity * Time.fixedDeltaTime;
        _rigidbody.MovePosition(currentPosition);
    }

    public void ProcessMovementInput(in Vector2 movementInput, in bool runInput)
    {
        float desiredHorizontalSpeed = 0f;

        if (runInput && !_playerStamina.WeAreFatigued)
            desiredHorizontalSpeed = movementInput.x * _runSpeed;
        else
            desiredHorizontalSpeed = movementInput.x * _walkSpeed;

        _currentVelocity.x = Mathf.MoveTowards(_currentVelocity.x, desiredHorizontalSpeed, _movementAcc * Time.deltaTime);
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

    private void UpdateBreathlessVFXSettings()
    {
        if (!_playerStamina.IsBreathless)
        {
            _muzzle.gameObject.SetActive(false);
            return;
        }

        _muzzle.gameObject.SetActive(true);

        float offsetX;
        float newRotY;

        if (IsFacingRight)
        {
            offsetX = _muzzleOffsetX;
            newRotY = 90.0f;
        }
        else
        {
            offsetX = -_muzzleOffsetX;
            newRotY = -90.0f;
        }

        Vector3 pos = _muzzle.localPosition;
        pos.x = offsetX;
        _muzzle.localPosition = pos;
        _breathlessVFX.localRotation = Quaternion.Euler(0f, newRotY, 0f);
    }
}
