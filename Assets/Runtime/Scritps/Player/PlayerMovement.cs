using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : CharacterMovement
{
    private PlayerStamina _playerStamina;

    public bool IsBreathless => _playerStamina.IsBreathless;

    protected override void Awake()
    {
        base.Awake();

        _playerStamina = GetComponent<PlayerStamina>();
    }

    protected override float SpeedHandler(in bool runInput)
    {
        if (_playerStamina.WeAreFatigued) return _walkSpeed;
        
        return base.SpeedHandler(runInput);
    }
}
