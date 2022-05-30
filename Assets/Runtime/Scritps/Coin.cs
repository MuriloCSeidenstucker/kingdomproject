using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] private float _bounciness = 0.4f;
    [Range(1, 4, order = 1)]
    [SerializeField] private int _bouncinessLimit = 3;

    private Vector2 _gravity = new Vector2(0f, -9.81f);
    private Vector2 _ground = new Vector2(0f, -2.9f);
    private Vector2 _currentVelocity;
    private int _boucinessCount;
    private bool _movementEnded;

    private void FixedUpdate()
    {
        if (!_movementEnded)
            ProcessNaturalMovement();
    }

    private void ProcessNaturalMovement()
    {
        _currentVelocity += _gravity * Time.fixedDeltaTime;

        Vector2 previousPosition = transform.position;
        if (previousPosition.y <= _ground.y)
        {
            ApllyBounciness(ref previousPosition);
        }
        Vector2 currentPosition = previousPosition + _currentVelocity * Time.fixedDeltaTime;

        if (_boucinessCount != _bouncinessLimit)
            transform.position = currentPosition;
        else
            _movementEnded = true;
    }

    private Vector2 ApllyBounciness(ref Vector2 previousPos)
    {
        _boucinessCount++;

        Vector2 velocityAfterCollision = _currentVelocity * _bounciness;
        _currentVelocity = -velocityAfterCollision;

        previousPos.y = _ground.y + 0.1f;
        return previousPos;
    }
}
