using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    
    private CharacterMovement _enemyMovement;

    private void Awake()
    {
        _enemyMovement = GetComponent<CharacterMovement>();
    }

    private void Update()
    {
        Vector2 direction = _player.transform.position - transform.position;

        _enemyMovement.ProcessMovementInput(direction.normalized, true);
    }
}
