using UnityEngine;

public class CoinInventory : MonoBehaviour, ICoinCollector
{
    [SerializeField] private GameHandler _gameHandler;
    [SerializeField] private CoinCollectorData _collectorData;
    [SerializeField] private int _fullInventory = 50;

    private int _coinAmount;
    private int _excessInventory;

    public int FullInventory { get { return _fullInventory;  } }
    public int CoinAmount { get { return _coinAmount; } private set { _coinAmount = Mathf.Max(0, value); } }

    private void Update()
    {
        ThrowExcessCoin();
    }

    private void ThrowCoin(in float throwingForce)
    {
        Coin coin = _gameHandler.GetFromCoinPool(_collectorData.CoinCollector.position, Quaternion.identity);
        coin.ApllyVelocityAfterThrown(throwingForce);
    }

    private void ThrowExcessCoin()
    {
        if (_excessInventory < 1) return;

        _excessInventory--;
        ThrowCoin(5.0f);
    }

    public void ThrowCoinFromInventory(in float throwingForce)
    {
        if (_coinAmount < 1) return;

        CoinAmount--;
        ThrowCoin(throwingForce);
    }

    public void ReactToCoinCollisionEnter()
    {
        if (CoinAmount < _fullInventory)
        {
            CoinAmount++;
        }
        else
        {
            _excessInventory++;
        }
    }

    public CoinCollectorData ReactToCoinCollisionStay()
    {
        return _collectorData;
    }
}
