using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class PlayerController : MonoBehaviour
{
    private CharacterMovement charMovement;
    private PlayerInputActions inputActions;

    public bool IsFacingRight => charMovement.IsFacingRight;

    private void Awake()
    {
        charMovement = GetComponent<CharacterMovement>();

        inputActions = new PlayerInputActions();
        inputActions.PlayerControls.Enable();
    }

    private void Update()
    {
        Vector2 movementInput = inputActions.PlayerControls.Movement.ReadValue<Vector2>();
        bool isRunning = inputActions.PlayerControls.Run.ReadValue<float>() != 0f;
        charMovement.ProcessMovementInput(in movementInput, in isRunning);
    }
}
