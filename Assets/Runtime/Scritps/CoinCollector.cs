using UnityEngine;

// Is a class needed to perform just that function?
// TODO: Check performance impact.
public class CoinCollector : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _colliderParent;
    [SerializeField] private float _attractSpeed = 8.0f;
    [SerializeField] private float _scaleSpeed = 2.0f;
    [SerializeField] private float _finalScaleMultiplier = 0.5f;

    private BoxCollider2D _myCollider;

    private void Awake()
    {
        _myCollider = GetComponent<BoxCollider2D>();
        _myCollider.size = _colliderParent.size;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Coin coin = other.GetComponent<Coin>();
        if (coin != null
            && coin.NaturalMovementEnded)
        {
            coin.ActivateCoinBehavior(_attractSpeed, _scaleSpeed, _finalScaleMultiplier, transform.position);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, _myCollider.size);
        }
    }
#endif
}
