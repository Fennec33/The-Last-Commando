using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public abstract class EnemyController : MonoBehaviour, IPooledGameObject
{
    [SerializeField] private int maxhealth = 1;
    [SerializeField] private int maxSpeed = 5;

    private GameObjectPool _pool;

    private int _health = 1;

    public float MaxSpeed { get { return maxSpeed; } }

    private void OnEnable()
    {
        _health = maxhealth;
        GetComponent<AIPath>().maxSpeed = maxSpeed;
    }

    public GameObjectPool Pool
    {
        get { return _pool; }
        set
        {
            if (_pool == null)
            {
                _pool = value;
            }
            else
            {
                throw new System.Exception("Bad pool use, this should only get set once!");
            }
        }
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
    
        if (_health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (_pool == null) { Destroy(this.gameObject); }
        else
        _pool.ReturnToPool(this.gameObject);
    }
}
