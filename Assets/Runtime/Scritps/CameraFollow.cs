using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;

    void LateUpdate()
    {
        Vector3 position = transform.position;
        position.x = target.position.x;
        transform.position = position;
    }
}
