using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator an;
    private PlayerController player;

    private const string Velocity = "Velocity";
    private const string IsRunning = "IsRunning";

    private void Awake()
    {
        an = GetComponent<Animator>();
        player = GetComponentInParent<PlayerController>();
    }

    private void LateUpdate()
    {
        an.SetFloat(Velocity, player.MovementInput.x);
        an.SetBool(IsRunning, player.IsRunning);
    }
}
