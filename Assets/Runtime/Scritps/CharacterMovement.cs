using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[DisallowMultipleComponent]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 10.0f;
    [SerializeField] private float runSpeed = 10.0f;
    [SerializeField] private float movementAcc = 100.0f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private Vector2 currentVelocity;
    public Vector2 CurrentVelocity { get { return currentVelocity; } }
    public float WalkSpeed { get { return walkSpeed; } }

    public bool IsFacingRight => spriteRenderer.flipX == false;

    //TODO: Específico do player.
    private PlayerStamina playerStamina;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        playerStamina = GetComponent<PlayerStamina>();
    }

    private void Update()
    {
        FlipSprite();
    }

    private void FixedUpdate()
    {
        Vector2 previousPosition = rb.position;
        Vector2 currentPosition = previousPosition + currentVelocity * Time.fixedDeltaTime;
        rb.MovePosition(currentPosition);
    }

    public void ProcessMovementInput(in Vector2 movementInput, in bool runInput)
    {
        float desiredHorizontalSpeed = 0f;

        if (runInput && !playerStamina.WeAreFatigued)
            desiredHorizontalSpeed = movementInput.x * runSpeed;
        else
            desiredHorizontalSpeed = movementInput.x * walkSpeed;

        currentVelocity.x = Mathf.MoveTowards(currentVelocity.x, desiredHorizontalSpeed, movementAcc * Time.deltaTime);
    }

    private void FlipSprite()
    {
        //TODO: Verificar se é possível reduzir os if.
        if (spriteRenderer != null)
        {
            if (CurrentVelocity.x > 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (CurrentVelocity.x < 0)
            {
                spriteRenderer.flipX = true;
            }
        }
    }
}
