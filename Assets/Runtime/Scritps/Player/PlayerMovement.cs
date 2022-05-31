using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : CharacterMovement
{
    [SerializeField] private Transform _breathlessVFX;
    [SerializeField] private Transform _muzzle;
    [SerializeField] private float _muzzleOffsetX = 0.4f;
    private PlayerStamina _playerStamina;

    protected override void Awake()
    {
        base.Awake();

        _playerStamina = GetComponent<PlayerStamina>();
    }

    protected override void Update()
    {
        base.Update();

        UpdateBreathlessVFXSettings();
    }

    protected override float SpeedHandler(in bool runInput)
    {
        if (_playerStamina.WeAreFatigued)
            return _walkSpeed;
        
        return base.SpeedHandler(runInput);
    }

    private void UpdateBreathlessVFXSettings()
    {
        if (!_playerStamina.IsBreathless)
        {
            _muzzle.gameObject.SetActive(false);
            return;
        }

        _muzzle.gameObject.SetActive(true);

        float offsetX;
        float newRotY;

        if (IsFacingRight)
        {
            offsetX = _muzzleOffsetX;
            newRotY = 90.0f;
        }
        else
        {
            offsetX = -_muzzleOffsetX;
            newRotY = -90.0f;
        }

        Vector3 pos = _muzzle.localPosition;
        pos.x = offsetX;
        _muzzle.localPosition = pos;
        _breathlessVFX.localRotation = Quaternion.Euler(0f, newRotY, 0f);
    }
}
