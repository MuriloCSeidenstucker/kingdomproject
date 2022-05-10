using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [Range(0f, 1f)] [SerializeField] private float bounciness = 0.4f;
    [Range(1, 4, order = 1)] [SerializeField] private int bouncinessLimit = 3;

    private Vector2 gravity = new Vector2(0f, -9.81f);
    private Vector2 ground = new Vector2(0f, -2.9f);
    private Vector2 currentVelocity;
    private int boucinessCount;
    private bool movementEnded;

    private void FixedUpdate()
    {
        if (!movementEnded)
            ProcessNaturalMovement();
    }

    private void ProcessNaturalMovement()
    {
        currentVelocity += gravity * Time.fixedDeltaTime;

        Vector2 previousPosition = transform.position;
        if (previousPosition.y <= ground.y)
        {
            ApllyBounciness(ref previousPosition);
        }
        Vector2 currentPosition = previousPosition + currentVelocity * Time.fixedDeltaTime;

        if (boucinessCount != bouncinessLimit)
            transform.position = currentPosition;
        else
            movementEnded = true;
    }

    private Vector2 ApllyBounciness(ref Vector2 previousPos)
    {
        boucinessCount++;

        Vector2 velocityAfterCollision = currentVelocity * bounciness;
        currentVelocity = -velocityAfterCollision;

        previousPos.y = ground.y + 0.1f;
        return previousPos;
    }
}
