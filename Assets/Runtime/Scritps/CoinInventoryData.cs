using UnityEngine;

[System.Serializable]
public struct CoinInventoryData
{
    private int _fullInventory;
    private int _coinAmount;
    private int _excessInventory;

    public int FullInventory { get { return _fullInventory; } }
    public int ExcessInventory { get { return _excessInventory; } set { _excessInventory = Mathf.Max(0, value); } }
    public int CoinAmount { get { return _coinAmount; } set { _coinAmount = Mathf.Max(0, value); } }
}
