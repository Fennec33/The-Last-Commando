using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour, IPooledGameObject
{
    [SerializeField] int healthRestored;
    [SerializeField] int grenadesRestored;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player.Heal(healthRestored);
            collision.gameObject.GetComponent<Shooter>().AddGrenades(grenadesRestored);
            _pool.ReturnToPool(this.gameObject);
        }
    }

    public void Clear()
    {
        _pool.ReturnToPool(this.gameObject);
    }
}
