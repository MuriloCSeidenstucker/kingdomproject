using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public Pool<Coin> _coinPool;

    private void Awake()
    {
        _coinPool.Initialize();
    }

    private void Start()
    {
        Vector3 pos = new Vector3(2.0f, 0.5f, 0f);
        for (int i = 0; i < 10; i++)
        {
            _coinPool.GetFromPool(pos, Quaternion.identity, transform);
            pos.x += 0.5f;
        }
    }
}
