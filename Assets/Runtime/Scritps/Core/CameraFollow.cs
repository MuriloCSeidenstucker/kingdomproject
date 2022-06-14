using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private PlayerController _player;

    [Header("Settings")]
    [Range(1, 4)]
    [SerializeField] private float _camTargetOffsetX = 2.0f;
    [SerializeField] private float _idleTime = 3.0f;
    [SerializeField] private float _speedToIdlePos = 2.0f;
    [SerializeField] private float _speedToCenterPos = 8.0f;

    private float _timer;
    private float _centerPos;
    private bool _keepLookingToRight;

    private void Start()
    {
        _keepLookingToRight = _player.IsFacingRight;
    }

    private void Update()
    {
        UpdateTargetPosition();
    }

    private void LateUpdate()
    {
        UpdateCameraPosition();
    }

    private void UpdateCameraPosition()
    {
        Vector3 position = transform.position;
        position.x = _target.position.x;
        transform.position = position;
    }

    private void UpdateTargetPosition()
    {
        Vector3 targetPos = _target.localPosition;
        float targetOffsetX = _player.IsFacingRight ? _camTargetOffsetX : -_camTargetOffsetX;

        targetPos.x = PlayerRemainsWithIdleOrientation()
            ? targetPos.x = Mathf.MoveTowards(targetPos.x, targetOffsetX, _speedToIdlePos * Time.deltaTime)
            : targetPos.x = Mathf.MoveTowards(targetPos.x, _centerPos, _speedToCenterPos * Time.deltaTime);

        _target.localPosition = targetPos;
    }

    private bool PlayerRemainsWithIdleOrientation()
    {
        if (_keepLookingToRight != _player.IsFacingRight)
        {
            _timer = 0f;
            _keepLookingToRight = _player.IsFacingRight;
            return false;
        }

        _timer += Time.deltaTime;

        return _timer >= _idleTime;
    }
}
