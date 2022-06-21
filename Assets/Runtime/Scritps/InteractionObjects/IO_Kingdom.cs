using UnityEngine;

public class IO_Kingdom : InteractionObject
{
    [SerializeField] private GameObject _particles;

    protected override bool BehaviorStarted()
    {
        return _particles.activeSelf;
    }

    protected override void PerformBehavior()
    {
        _particles.SetActive(true);
    }
}
