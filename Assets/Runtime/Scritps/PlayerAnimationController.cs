using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator an;
    private CharacterMovement charMovement;

    private const string Velocity = "Velocity";

    private void Awake()
    {
        an = GetComponent<Animator>();
        charMovement = GetComponentInParent<CharacterMovement>();
    }

    private void LateUpdate()
    {
        an.SetFloat(Velocity, charMovement.AnimVelocity);
    }
}
