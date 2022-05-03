using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private float speedToReset = 8.0f;

    private float timer = 0f;

    private bool keepLookingToRight;
    private bool cameraIsInIdlePosition;

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

        if (PlayerRemainsWithIdleOrientation() && !cameraIsInIdlePosition)
        {
            targetPos.x = Mathf.MoveTowards(targetPos.x, targetOffsetX, speedToIdlePos * Time.deltaTime);
            cameraIsInIdlePosition = target.localPosition.x == targetOffsetX;
        }
        else if (PlayerRemainsWithIdleOrientation() && cameraIsInIdlePosition)
        {
            targetPos.x = targetOffsetX;
        }
        else
        {
            targetPos.x = 0f;
        }

        target.localPosition = targetPos;
    }

    private bool PlayerRemainsWithIdleOrientation()
    {
        if (keepLookingToRight != player.IsFacingRight)
        {
            timer = 0f;
            keepLookingToRight = player.IsFacingRight;
            cameraIsInIdlePosition = false;
            return false;
        }

        timer += Time.deltaTime;

        return timer >= idleTime;
    }
}
