using UnityEngine;

[System.Serializable]
public struct CoinCollectorData
{
    public Transform CoinCollector;
    public float AttractSpeed;
    public float ScaleSpeed;
    public float FinalScaleMultiplier;

    public CoinCollectorData(float attractSpeed, float scaleSpeed, float finalScaleMultiplier, Transform coinCollector)
    {
        CoinCollector = coinCollector;
        AttractSpeed = attractSpeed;
        ScaleSpeed = scaleSpeed;
        FinalScaleMultiplier = finalScaleMultiplier;
    }
}
