using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterMovement))]
public class PlayerStamina : MonoBehaviour
{
    [SerializeField] private float staminaRegenValue = 5.0f;
    [SerializeField] private float normalRegenSpeed = 2.0f;
    [Space]
    [SerializeField] private float staminaDrainValue = 5.0f;
    [Range(0.1f, 0.5f)] [SerializeField] private float fatiguePercent = 0.3f;
    [Space]
    [SerializeField] private Text staminaText;

    private CharacterMovement charMovement;

    private float maxStamina = 100.0f;
    private float currentStamina;
    private bool weAreFatigued;

    private bool weAreSprinting => charMovement.CurrentVelocity.magnitude > charMovement.WalkSpeed;

    public bool WeAreFatigued { get { return weAreFatigued; } }

    public bool IsBreathless
    {
        get
        {
            if (weAreFatigued)
                return true;
            else
                return currentStamina <= maxStamina * fatiguePercent;
        }
    }

    private void Awake()
    {
        charMovement = GetComponent<CharacterMovement>();
        currentStamina = maxStamina;
    }

    private void Update()
    {
        if (weAreSprinting)
        {
            StaminaDrain();
        }
        else
        {
            StaminaRegeneration();
        }

    }

    private void LateUpdate()
    {
        staminaText.text = $"{currentStamina}%";
    }

    private void StaminaDrain()
    {
        if (currentStamina <= 0f)
        {
            weAreFatigued = true;
            return;
        }

        currentStamina -= staminaDrainValue * Time.deltaTime;
    }

    private void StaminaRegeneration()
    {
        if (currentStamina > maxStamina - 0.01f)
        {
            weAreFatigued = false;
            return;
        }

        currentStamina += staminaRegenValue * normalRegenSpeed * Time.deltaTime;
    }
}
