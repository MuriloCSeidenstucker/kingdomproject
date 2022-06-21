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
    public int ExcessInventory { get { return _excessInventory; } private set { _excessInventory = Mathf.Max(0, value); } }

    private void Update()
    {
        ThrowExcessCoin();
    }

    private void TryAddCoin(int value = 1)
    {
        if (CoinAmount < FullInventory)
        {
            CoinAmount += value;
        }
        else
        {
            ExcessInventory += value;
        }
    }

    private void ThrowCoin(in float throwingForce)
    {
        Coin coin = _gameHandler.GetFromCoinPool(_collectorData.CoinCollector.position, Quaternion.identity);
        coin.ApllyVelocityAfterThrown(throwingForce);
    }

    private void ThrowExcessCoin()
    {
        if (ExcessInventory < 1) return;

        ExcessInventory--;
        ThrowCoin(throwingForce: 5.0f);
    }

    public void ThrowCoinFromInventory(in float throwingForce)
    {
        if (_coinAmount < 1) return;

        CoinAmount--;
        ThrowCoin(throwingForce);
    }

    public void Purchase(in int value)
    {
        CoinAmount -= value;
    }

    public void ReactToCoinCollisionEnter() => TryAddCoin();

    public CoinCollectorData ReactToCoinCollisionStay() => _collectorData;
}
