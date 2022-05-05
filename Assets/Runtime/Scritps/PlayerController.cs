using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class PlayerController : MonoBehaviour
{
    private CharacterMovement charMovement;
    private PlayerInputActions inputActions;

    private Vector2 movementInput;
    public Vector2 MovementInput { get { return movementInput; } }

    private bool isRunning;
    public bool IsRunning { get { return isRunning; } }

    public bool IsFacingRight => charMovement.IsFacingRight;

    private void Awake()
    {
        charMovement = GetComponent<CharacterMovement>();

        inputActions = new PlayerInputActions();
        inputActions.PlayerControls.Enable();
    }

    private void Update()
    {
        movementInput = inputActions.PlayerControls.Movement.ReadValue<Vector2>();
        isRunning = inputActions.PlayerControls.Run.ReadValue<float>() != 0f;
        charMovement.ProcessMovementInput(in movementInput, in isRunning);
    }
}
