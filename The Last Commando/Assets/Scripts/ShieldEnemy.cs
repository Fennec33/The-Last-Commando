using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ShieldEnemy : EnemyController
{
    [SerializeField] private float attackRange = 27f;
    [SerializeField] private float shootZone = 5f;

    private Transform _playerTransform;
    private PlayerController _playerController;
    private AIDestinationSetter _aIDestinationSetter;

    private bool _isWithinRange = false;
    private bool _canSeePlayer = false;

    private void Awake()
    {
        _playerController = FindObjectOfType<PlayerController>();
        _playerTransform = _playerController.GetComponent<Transform>();
        _aIDestinationSetter = GetComponent<AIDestinationSetter>();
        _aIDestinationSetter.target = _playerTransform;
    }

    private void Update()
    {
        BehaviorStateMachine();
    }

    private void BehaviorStateMachine()
    {
        if (_isWithinRange)
        {
            GetComponent<AIPath>().maxSpeed = 0.1f;

            if (Vector3.Distance(transform.position, _playerTransform.position) - attackRange >= shootZone)
            {
                _isWithinRange = false;
            }
        }
        else
        {
            GetComponent<AIPath>().maxSpeed = MaxSpeed;

            if (Vector3.Distance(transform.position, _playerTransform.position) < attackRange)
            {
                _isWithinRange = true;
            }
        }
    }
}
