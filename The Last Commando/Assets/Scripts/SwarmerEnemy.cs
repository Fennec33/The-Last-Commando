using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SwarmerEnemy : EnemyController
{
    [SerializeField] private int damage;
    [SerializeField] private float attackRange;

    private Transform _playerTransform;
    private PlayerController _playerController;
    private AIDestinationSetter _aIDestinationSetter;

    private Vector3 _offset; // The offset causes the Swarmers to come after the player in a less uniform way
    private GameObject _target;
    private bool _isCloseToTarget = false;
    private float chaseDistance = 6f;

    private void Start()
    {
        _offset = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0f);
        _target = new GameObject("SwarmerEnemyTarget");
    }

    private void Awake()
    {
        _playerController = FindObjectOfType<PlayerController>();
        _playerTransform = _playerController.GetComponent<Transform>();
        _aIDestinationSetter = GetComponent<AIDestinationSetter>();
    }

    void Update()
    {
        _target.transform.position = _playerTransform.position + _offset;

        if (Vector3.Distance(transform.position, _playerTransform.position) <= attackRange)
        {
            Attack();
        }

        if (Vector3.Distance(transform.position, _playerTransform.position) <= chaseDistance)
        {
            _isCloseToTarget = true;
        }
        else
        {
            _isCloseToTarget = false;
        }

        BehaviorStateMachine();
    }

    private void BehaviorStateMachine()
    {
        if (_isCloseToTarget)
        {
            _aIDestinationSetter.target = _playerTransform;
        }
        else
        {
            _aIDestinationSetter.target = _target.transform;
        }
    }

    private void Attack()
    {
        _playerController.TakeDamage(damage);
    }

    private void OnDestroy()
    {
        Destroy(_target);
    }
}
