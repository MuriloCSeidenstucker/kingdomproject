using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 10.0f;
    [SerializeField] private float acceleration = 100.0f;

    private Rigidbody2D rb;
    private Vector2 currentVelocity;

    public Vector2 CurrentVelocity { get { return currentVelocity; } }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 previousPosition = rb.position;
        Vector2 currentPosition = previousPosition + currentVelocity * Time.fixedDeltaTime;
        rb.MovePosition(currentPosition);
    }

    public void ProcessMovementInput(Vector2 movementInput)
    {
        //TODO: Implementar também a corrida.
        float desiredHorizontalSpeed = movementInput.x * walkSpeed;
        currentVelocity.x = Mathf.MoveTowards(currentVelocity.x, desiredHorizontalSpeed, acceleration * Time.deltaTime);
    }
}
