using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IO_Wall : InteractionObject
{
    private Wall _wall;

    protected override bool BehaviorEnded => _wall.Level > 0;

    private void Awake()
    {
        _wall = GetComponentInParent<Wall>();
    }

    protected override void PerformBehavior()
    {
        _wall.LevelUp();
    }
}
