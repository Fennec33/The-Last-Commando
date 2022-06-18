using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour, IPooledGameObject
{
    [SerializeField] private int damage = 1;

    private GameObjectPool _pool;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }

        EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
        if (enemy == null)
        {
            _pool.ReturnToPool(this.gameObject);
        }
    }

    private void Update()
    {
        if (transform.position.magnitude >= 100)
        {
            _pool.ReturnToPool(this.gameObject);
        }
    }
}
