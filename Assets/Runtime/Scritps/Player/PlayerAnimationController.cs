using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator _animator;
    private CharacterMovement _charMovement;

    public float PlayerCurrentVelocity => _charMovement.CurrentVelocity.magnitude;
    public float PlayerWalkSpeed => _charMovement.WalkSpeed;

    public float PlayerVelocity
    {
        get
        {
            if (PlayerCurrentVelocity != 0f)
            {
                if (PlayerCurrentVelocity <= PlayerWalkSpeed)
                    return c_walkAnim;
                else
                    return c_runAnim;
            }
            return c_idleAnim;
        }
    }

    private const string c_velocity = "Velocity";
    private const float c_idleAnim = 0f;
    private const float c_walkAnim = 0.5f;
    private const float c_runAnim = 1.0f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _charMovement = GetComponentInParent<CharacterMovement>();
    }

    private void LateUpdate()
    {
        _animator.SetFloat(c_velocity, PlayerVelocity);
    }
}
