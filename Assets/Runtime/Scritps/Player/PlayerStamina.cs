using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerStamina : MonoBehaviour
{
    [SerializeField] private float _staminaRegenValue = 5.0f;
    [SerializeField] private float _normalRegenSpeed = 2.0f;

    [Space]
    [SerializeField] private float _staminaDrainValue = 5.0f;
    [Range(0.1f, 1f)]
    [SerializeField] private float _breathlessPercent = 0.3f;

    private PlayerMovement _playerMovement;
    private float _maxStamina = 100.0f;
    [SerializeField] private float _currentStamina;
    private bool _weAreFatigued;

    public bool WeAreFatigued { get { return _weAreFatigued; } }
    public bool IsBreathless { get { return _weAreFatigued || _currentStamina <= _maxStamina * _breathlessPercent; } }

    public bool WeAreSprinting => _playerMovement.CurrentVelocity.magnitude > _playerMovement.WalkSpeed;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _currentStamina = _maxStamina;
    }

    private void Update()
    {
        if (WeAreSprinting)
        {
            StaminaDrain();
        }
        else
        {
            StaminaRegeneration();
        }
    }

    private void StaminaDrain()
    {
        if (_currentStamina <= 0f)
        {
            _weAreFatigued = true;
            return;
        }

        _currentStamina -= _staminaDrainValue * Time.deltaTime;
    }

    private void StaminaRegeneration()
    {
        if (_currentStamina > _maxStamina - 0.01f)
        {
            _weAreFatigued = false;
            return;
        }

        _currentStamina += _staminaRegenValue * _normalRegenSpeed * Time.deltaTime;
    }
}
