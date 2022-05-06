using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator an;
    private CharacterMovement charMovement;

    private float playerCurrentVelocity => charMovement.CurrentVelocity.magnitude;
    private float playerWalkSpeed => charMovement.WalkSpeed;

    private float PlayerVelocity
    {
        get
        {
            if (playerCurrentVelocity != 0f)
            {
                if (playerCurrentVelocity <= playerWalkSpeed)
                    return walkAnim;
                else
                    return runAnim;
            }
            return idleAnim;
        }
    }

    private const string Velocity = "Velocity";
    private const float idleAnim = 0f;
    private const float walkAnim = 0.5f;
    private const float runAnim = 1.0f;

    private void Awake()
    {
        an = GetComponent<Animator>();
        charMovement = GetComponentInParent<CharacterMovement>();
    }

    private void LateUpdate()
    {
        an.SetFloat(Velocity, PlayerVelocity);
    }
}
