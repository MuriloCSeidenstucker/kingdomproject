using TMPro;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour, ICoinCollector
{
    [SerializeField] private CoinCollectorData _collectorData;
    
    private PlayerMovement _playerMovement;
    private PlayerInputActions _inputActions;

    [SerializeField] private TextMeshProUGUI _coinAmountText;
    private int _coinInventory;

    public bool IsFacingRight => _playerMovement.IsFacingRight;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();

        _inputActions = new PlayerInputActions();
        _inputActions.PlayerControls.Enable();
    }

    private void Update()
    {
        Vector2 movementInput = _inputActions.PlayerControls.Movement.ReadValue<Vector2>();
        bool runInput = _inputActions.PlayerControls.Run.IsPressed();

        _playerMovement.ProcessMovementInput(in movementInput, in runInput);

        if (movementInput.x != 0f)
            _playerMovement.PreventPlayerRun(_inputActions.PlayerControls.Run.WasPressedThisFrame());
    }

    private void LateUpdate()
    {
        _coinAmountText.text = $"{_coinInventory}/50";
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Coin coin = other.GetComponent<Coin>();
        if (coin != null && coin.NaturalMovementEnded)
        {
            _coinInventory++;
        }
    }

    public CoinCollectorData ReactToCoinCollision()
    {
        return _collectorData;
    }
}
