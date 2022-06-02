using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private PlayerInputActions _inputActions;

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
        _playerMovement.StopPlayerMovement(_inputActions.PlayerControls.Run.WasPressedThisFrame());
    }
}
