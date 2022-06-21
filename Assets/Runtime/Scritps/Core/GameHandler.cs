using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private Transform _coinPoolParent;
    [SerializeField] private Pool<Coin> _coinPool;

    private void Awake()
    {
        Application.targetFrameRate = -1;
        _coinPool.Initialize();
    }

    public Coin GetFromCoinPool(Vector3 position, Quaternion rotation)
    {
        return _coinPool.GetFromPool(position, rotation, _coinPoolParent);
    }

    public void ReturnToCoinPool(Coin coin)
    {
        _coinPool.ReturnToPool(coin);
    }
}
