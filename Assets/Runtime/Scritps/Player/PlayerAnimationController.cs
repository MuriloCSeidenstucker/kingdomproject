using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator _animator;
    private PlayerMovement _playerMovement;
    private ParticleSystem _breathlessVFX;

    public float PlayerCurrentVelocity => _playerMovement.CurrentVelocity.magnitude;
    public float PlayerWalkSpeed => _playerMovement.WalkSpeed;

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
    private const string c_isStopped = "IsStopped";
    private const float c_idleAnim = 0f;
    private const float c_walkAnim = 0.5f;
    private const float c_runAnim = 1.0f;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _playerMovement = GetComponentInParent<PlayerMovement>();
        _breathlessVFX = GetComponentInChildren<ParticleSystem>();
    }

    private void Update()
    {
        UpdateBreathlessVFX();
    }

    private void LateUpdate()
    {
        _animator.SetFloat(c_velocity, PlayerVelocity);

        // It is not in sync with the gameplay.
        _animator.SetBool(c_isStopped, _playerMovement.IsStopped);
    }

    private void UpdateBreathlessVFX()
    {
        float newRot = _playerMovement.IsFacingRight ? 90f : -90f;

        if (_breathlessVFX.particleCount == 0)
        {
            _breathlessVFX.transform.localRotation = Quaternion.Euler(0f, newRot, 0f);
        }

        if (_playerMovement.IsBreathless && !_breathlessVFX.isPlaying)
        {
            _breathlessVFX.Play();
        }

        if (!_playerMovement.IsBreathless && _breathlessVFX.particleCount == 0)
        {
            _breathlessVFX.Stop();
        }
    }
}
