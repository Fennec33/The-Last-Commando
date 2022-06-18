using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ArtilleryEnemy : EnemyController
{
    [SerializeField] private float attackRange = 20f;
    [SerializeField] private float shootZone = 3f;
    [SerializeField] private float shootTime = 4f;

    [SerializeField] private GameObject targeter;

    private Transform _playerTransform;
    private PlayerController _playerController;
    private AIDestinationSetter _aIDestinationSetter;

    private bool _isWithinRange = false;
    private float _shootTimer = 0f;

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

            _shootTimer += Time.deltaTime;

            if(_shootTimer >= shootTime)
            {
                _shootTimer = 0f;

                Instantiate(targeter, _playerTransform.position, Quaternion.identity);
            }

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
