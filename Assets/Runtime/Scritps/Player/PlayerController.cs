using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class PlayerController : MonoBehaviour
{
    private CharacterMovement _charMovement;
    private PlayerInputActions _inputActions;

    public bool IsFacingRight => _charMovement.IsFacingRight;

    private void Awake()
    {
        _charMovement = GetComponent<CharacterMovement>();

        _inputActions = new PlayerInputActions();
        _inputActions.PlayerControls.Enable();
    }

    private void Update()
    {
        Vector2 movementInput = _inputActions.PlayerControls.Movement.ReadValue<Vector2>();
        bool runInput = _inputActions.PlayerControls.Run.ReadValue<float>() != 0f;
        _charMovement.ProcessMovementInput(in movementInput, in runInput);
    }
}
