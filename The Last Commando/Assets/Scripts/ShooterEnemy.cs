using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ShooterEnemy : EnemyController
{
    [SerializeField] private float shootSpeed = 1f;
    [SerializeField] private float shootOffset = 0.2f;
    [SerializeField] private float attackRange = 25f;
    [SerializeField] private float shootZone = 5f;
    [SerializeField] private float inacuracy = 0.1f;
    [SerializeField] private float bulletForce = 20f;

    [SerializeField] GameObjectPool bulletPool;

    private Transform _playerTransform;
    private PlayerController _playerController;
    private AIDestinationSetter _aIDestinationSetter;
    private ShooterAnimations _animations;
    private Transform _leftShootPoint;
    private Transform _rightShootPoint;

    private bool _isShooting = false;
    private bool _canSeePlayer = false;
    private float _shootTimer = 0f;
    private bool _waitingForSecondShot = false;
    private int _playerMask;
    private int _obstacleMask;
    private int _combinedMask;

    private void Awake()
    {
        _playerController = FindObjectOfType<PlayerController>();
        _playerTransform = _playerController.GetComponent<Transform>();
        _aIDestinationSetter = GetComponent<AIDestinationSetter>();
        _aIDestinationSetter.target = _playerTransform;
        _animations = GetComponent<ShooterAnimations>();
        _leftShootPoint = this.transform.Find("FirePointL");
        _rightShootPoint = this.transform.Find("FirePointR");
        _playerMask = 1 << LayerMask.NameToLayer("Player");
        _obstacleMask = 1 << LayerMask.NameToLayer("Obstacle");
        _combinedMask = _playerMask | _obstacleMask;
    }

    private void Update()
    {
        BehaviorStateMachine();
    }

    private void BehaviorStateMachine()
    {
        RaycastHit2D _hit = Physics2D.Linecast(transform.position, _playerTransform.position, _combinedMask);
        if (_hit.collider != null)
        {
            if (_hit.collider.gameObject.CompareTag("Player"))
            {
                _canSeePlayer = true;
            }
            else
            {
                _canSeePlayer = false;
            }
        }

        if (_isShooting && _canSeePlayer)
        {
            GetComponent<AIPath>().maxSpeed = 0.1f;

            //TODO rotate toward player

            _shootTimer += Time.deltaTime;
            if (_shootTimer >= shootSpeed && _waitingForSecondShot == false)
            {
                Shoot(1);
                _waitingForSecondShot = true;
            }
            else if (_shootTimer >= shootSpeed + shootOffset)
            {
                Shoot(2);
                _waitingForSecondShot = false;
                _shootTimer = 0;
            }

            if (Vector3.Distance(transform.position, _playerTransform.position) - attackRange >= shootZone)
            {
                _shootTimer = 0;
                _isShooting = false;
            }
        }
        else
        {
            GetComponent<AIPath>().maxSpeed = MaxSpeed;

            
            if (Vector3.Distance(transform.position, _playerTransform.position) < attackRange)
            {
                _isShooting = true;
            }
        }
    }

    private void Shoot(int _gun)
    {
        Transform shootPoint;
        if(_gun == 1)
        {
            shootPoint = _leftShootPoint;
            _animations.AnimateRecoil(1);
        }
        else
        {
            shootPoint = _rightShootPoint;
            _animations.AnimateRecoil(2);
        }

        var bullet = bulletPool.Get();
        bullet.transform.position = shootPoint.position;
        bullet.transform.rotation = shootPoint.rotation;
        Vector2 _shootDirection = AddInacuracy(_leftShootPoint.up);

        bullet.SetActive(true);
        bullet.GetComponent<Rigidbody2D>().AddForce(_shootDirection * bulletForce, ForceMode2D.Impulse);
    }

    private Vector2 AddInacuracy(Vector2 vector)
    {
        vector.x = vector.x + Random.value * inacuracy - inacuracy / 2;
        vector.y = vector.y + Random.value * inacuracy - inacuracy / 2;

        return vector;
    }
}
