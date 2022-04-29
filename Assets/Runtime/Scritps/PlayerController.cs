using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class PlayerController : MonoBehaviour
{
    private CharacterMovement charMovement;
    private PlayerInputActions inputActions;

    private void Awake()
    {
        charMovement = GetComponent<CharacterMovement>();
        inputActions = new PlayerInputActions();
        inputActions.PlayerControls.Enable();
    }

    private void Update()
    {
        Vector2 frameinput = inputActions.PlayerControls.Movement.ReadValue<Vector2>();
        charMovement.ProcessMovementInput(frameinput);
    }
}
