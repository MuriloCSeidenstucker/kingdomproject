using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private PlayerController player;

    [Header("Settings")]
    [Range(1, 4)]
    [SerializeField] private float camTargetOffsetX = 2.0f;
    [SerializeField] private float idleTime = 3.0f;
    [SerializeField] private float speedToIdlePos = 2.0f;
    [SerializeField] private float speedToCenterPos = 8.0f;

    private float timer = 0f;
    private float centerPos = 0f;
    private bool keepLookingToRight;

    private void Start()
    {
        keepLookingToRight = player.IsFacingRight;
    }

    private void Update()
    {
        UpdateTargetPosition();
    }

    private void LateUpdate()
    {
        Vector3 position = transform.position;
        position.x = target.position.x;
        transform.position = position;
    }

    private void UpdateTargetPosition()
    {
        Vector3 targetPos = target.localPosition;
        float targetOffsetX = player.IsFacingRight ? camTargetOffsetX : -camTargetOffsetX;
        bool playerRemainsWithIdleOrientation = PlayerRemainsWithIdleOrientation();

        if (playerRemainsWithIdleOrientation && target.localPosition.x != targetOffsetX)
        {
            targetPos.x = Mathf.MoveTowards(targetPos.x, targetOffsetX, speedToIdlePos * Time.deltaTime);
        }
        else if (playerRemainsWithIdleOrientation)
        {
            targetPos.x = targetOffsetX;
        }
        else if (!playerRemainsWithIdleOrientation && target.localPosition.x != centerPos)
        {
            targetPos.x = Mathf.MoveTowards(targetPos.x, centerPos, speedToCenterPos * Time.deltaTime);
        }
        else
        {
            targetPos.x = centerPos;
        }

        target.localPosition = targetPos;
    }

    private bool PlayerRemainsWithIdleOrientation()
    {
        if (keepLookingToRight != player.IsFacingRight)
        {
            timer = 0f;
            keepLookingToRight = player.IsFacingRight;
            return false;
        }

        timer += Time.deltaTime;

        return timer >= idleTime;
    }
}
