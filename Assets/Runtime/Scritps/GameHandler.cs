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

    private void Start()
    {
        Vector3 pos = new Vector3(1.0f, 0.5f, 0f);
        for (int i = 0; i < 4; i++)
        {
            _coinPool.GetFromPool(pos, Quaternion.identity, _coinPoolParent);
            pos.x += 0.5f;
        }
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
