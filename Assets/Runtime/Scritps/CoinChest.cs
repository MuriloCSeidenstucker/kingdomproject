using UnityEngine;

public class CoinChest : MonoBehaviour
{
    [SerializeField] private GameHandler _gameHandler;

    private Animator _animator;

    private const string c_activateAnimation = "ActivateAnimation";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            _animator.SetTrigger(c_activateAnimation);
            Coin coin = _gameHandler.GetFromCoinPool(transform.position, Quaternion.identity);
            coin.ApllyVelocityAfterEjection();
        }
    }
}
