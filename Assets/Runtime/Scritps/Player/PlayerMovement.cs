using System.Collections;
using UnityEngine;

public class PlayerMovement : CharacterMovement
{
    private PlayerStamina _playerStamina;
    private bool _isStopped;

    // It is not in sync with the animation.
    private float _stoppedTime = 1.0f;

    public bool IsBreathless => _playerStamina.IsBreathless;
    public bool IsStopped => _isStopped;

    protected override void Awake()
    {
        base.Awake();

        _playerStamina = GetComponent<PlayerStamina>();
    }

    protected override float SpeedHandler(in bool runInput)
    {
        if (!_playerStamina.WeAreFatigued) return base.SpeedHandler(runInput);

        if (_isStopped) return 0f;

        return _walkSpeed;
    }

    private IEnumerator StopPlayerMovementCor()
    {
        _isStopped = true;

        yield return new WaitForSeconds(_stoppedTime);

        _isStopped = false;
    }

    public void StopPlayerMovement(in bool triedRun)
    {
        if (!_playerStamina.WeAreFatigued || !triedRun) return;

        if (!_isStopped)
            StartCoroutine(StopPlayerMovementCor());
    }
}
