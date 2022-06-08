using UnityEngine;

[System.Serializable]
public struct CoinCollectorData
{
    [SerializeField] public Transform CoinCollector;
    [SerializeField] public float AttractSpeed;
    [SerializeField] public float ScaleSpeed;
    [SerializeField] public float FinalScaleMultiplier;

    public CoinCollectorData(float attractSpeed, float scaleSpeed, float finalScaleMultiplier, Transform coinCollector)
    {
        CoinCollector = coinCollector;
        AttractSpeed = attractSpeed;
        ScaleSpeed = scaleSpeed;
        FinalScaleMultiplier = finalScaleMultiplier;
    }
}
