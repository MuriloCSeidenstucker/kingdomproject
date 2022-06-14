using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Coin), typeof(Collider2D))]
public class CoinCollision : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _fullCoin;

    private Coin _coin;
    private GameHandler _gameHandler;
    private Collider2D _myCollider;
    private Vector3 _coinCollectorPos;
    private float _attractSpeed;
    private float _scaleSpeed;
    private float _finalScaleMultiplier;

    private void Awake()
    {
        _coin = GetComponent<Coin>();
        _gameHandler = GetComponentInParent<GameHandler>();
        _myCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        _myCollider.enabled = _coin.NaturalMovementEnded;
        PerformCoinBehavior();
    }

    private void PerformCoinBehavior()
    {
        if (!_coin.ActivatedBehavior) return;

        _coin.Animator.enabled = false;
        _spriteRenderer.sprite = _fullCoin;

        Vector3 startPos = transform.position;
        Vector3 endPos = _coinCollectorPos;
        Vector3 startScale = transform.localScale;
        Vector3 endScale = Vector3.one * _finalScaleMultiplier;

        if (startPos != endPos)
        {
            transform.position = Vector3.MoveTowards(startPos, endPos, _attractSpeed * Time.deltaTime);
        }
        else if (startScale != endScale)
        {
            transform.localScale = Vector3.MoveTowards(startScale, endScale, _scaleSpeed * Time.deltaTime);
        }
        else
        {
            Assert.IsNotNull(_gameHandler, $"{_coin} instantiated outside the pool");
            _gameHandler.ReturnToCoinPool(_coin);
        }
    }

    private void ActivateCoinBehavior(in CoinCollectorData data)
    {
        _attractSpeed = data.AttractSpeed;
        _scaleSpeed = data.ScaleSpeed;
        _finalScaleMultiplier = data.FinalScaleMultiplier;
        _coinCollectorPos = data.CoinCollector.position;
        _coin.ActivatedBehavior = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ICoinCollector collector = other.GetComponent<ICoinCollector>();
        if (collector != null
            && _coin.NaturalMovementEnded
            && !_coin.WasCollected)
        {
            _coin.WasCollected = true;
            collector.ReactToCoinCollisionEnter();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        ICoinCollector collector = other.GetComponent<ICoinCollector>();
        if (collector != null && _coin.NaturalMovementEnded)
        {
            ActivateCoinBehavior(collector.ReactToCoinCollisionStay());
        }
    }
}
