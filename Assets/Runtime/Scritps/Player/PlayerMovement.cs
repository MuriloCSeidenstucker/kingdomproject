using System.Collections;
using UnityEngine;

public class PlayerMovement : CharacterMovement
{
    private PlayerStamina _playerStamina;
    private bool _isPreventedRun;

    // It is not in sync with the animation.
    private float _timePreventedRun = 1.0f;

    public bool IsBreathless => _playerStamina.IsBreathless;
    public bool IsPreventedRun => _isPreventedRun;

    protected override void Awake()
    {
        base.Awake();

        _playerStamina = GetComponent<PlayerStamina>();
    }

    protected override float SpeedHandler(in bool runInput)
    {
        if (!_playerStamina.WeAreFatigued && !_isPreventedRun) return base.SpeedHandler(runInput);

        if (_isPreventedRun) return 0f;

        return _walkSpeed;
    }

    private IEnumerator StopPlayerMovementCor()
    {
        _isPreventedRun = true;

        yield return new WaitForSeconds(_timePreventedRun);

        _isPreventedRun = false;
    }

    public void PreventPlayerRun(in bool triedRunInput)
    {
        if (!_playerStamina.WeAreFatigued || !triedRunInput) return;

        if (!_isPreventedRun)
            StartCoroutine(StopPlayerMovementCor());
    }
}
