using TMPro;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private PlayerInputActions _inputActions;
    private CoinInventory _coinInventory;

    // TODO: Create class to handle UI.
    [SerializeField] private TextMeshProUGUI _coinAmountText;

    public bool IsFacingRight => _playerMovement.IsFacingRight;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _coinInventory = GetComponent<CoinInventory>();

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

        if (_inputActions.PlayerControls.Throw.WasPressedThisFrame())
        {
            _coinInventory.ThrowCoinFromInventory(throwingForce: 3.0f);
        }
    }

    private void LateUpdate()
    {
        _coinAmountText.text = $"{_coinInventory.CoinAmount}/{_coinInventory.FullInventory}";
    }
}
