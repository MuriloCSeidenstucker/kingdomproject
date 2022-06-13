using System.Collections;
using UnityEngine;

public class CoinChest : MonoBehaviour
{
    [SerializeField] private GameHandler _gameHandler;
    [SerializeField] private Transform _ejectionPos;
    [SerializeField] private int _coinAmount = 5;

    private Animator _animator;
    private float _ejectionDelay = 0.3f;
    private bool _openChest;

    private const string c_activateAnimation = "ActivateAnimation";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null && !_openChest)
        {
            _openChest = true;
            _animator.SetTrigger(c_activateAnimation);
            StartCoroutine(OpenChestCor());
        }
    }

    private IEnumerator OpenChestCor()
    {
        for (int i = 0; i < _coinAmount; i++)
        {
            Coin coin = _gameHandler.GetFromCoinPool(_ejectionPos.position, Quaternion.identity);
            coin.ApllyVelocityAfterEjection(ejectionForce: 5.0f);
            yield return new WaitForSeconds(_ejectionDelay);
        }
    }
}
