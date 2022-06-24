using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IO_Tower : InteractionObject
{
    private Tower _tower;

    protected override bool BehaviorEnded => _tower.Level > 0;

    private void Awake()
    {
        _tower = GetComponentInParent<Tower>();
    }

    protected override void PerformBehavior()
    {
        _tower.LevelUp();
    }
}
