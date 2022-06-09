using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPooledObject
{
    public void OnInstantiated();
    public void OnEnabledFromPool();
    public void OnDisabledFromPool();
}

[System.Serializable]
public class Pool<T> where  T : MonoBehaviour, IPooledObject
{
    [SerializeField] private Transform _poolRoot;
    [SerializeField] private T _prefab;
    [SerializeField] private int _initialObjectCount;

    private List<T> _objects;

    public void Initialize()
    {
        _objects = new List<T>(_initialObjectCount);
        for (int i = 0; i < _initialObjectCount; i++)
        {
            _objects.Add(InstantiateObject());
        }
    }

    public T GetFromPool(Vector3 position, Quaternion rotation, Transform parent)
    {
        T obj;
        if (_objects.Count > 0)
        {
            obj = _objects[_objects.Count - 1];
            _objects.RemoveAt(_objects.Count - 1);
        }
        else
        {
            obj = InstantiateObject();
        }

        SetupObject(obj, position, rotation, parent);
        obj.gameObject.SetActive(true);
        obj.OnEnabledFromPool();

        return obj;
    }

    public void ReturnToPool(T obj)
    {
        if (obj != null)
        {
            obj.gameObject.SetActive(false);
            obj.transform.SetParent(_poolRoot);
            obj.OnDisabledFromPool();
            _objects.Add(obj);
        }
    }

    private T InstantiateObject()
    {
        var obj = GameObject.Instantiate(_prefab, _poolRoot);
        obj.OnInstantiated();
        obj.gameObject.SetActive(false);
        return obj;
    }

    private void SetupObject(T obj, Vector3 position, Quaternion rotation, Transform parent)
    {
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.transform.SetParent(parent);
    }
}
