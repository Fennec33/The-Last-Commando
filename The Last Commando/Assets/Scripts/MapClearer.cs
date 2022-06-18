using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapClearer : MonoBehaviour
{
    private BoxCollider2D collider;

    private void Start()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.TakeDamage(100);
        }

        Pickup pickup = collision.gameObject.GetComponent<Pickup>();
        if (pickup != null)
        {
            pickup.Clear();
        }
    }

    public void ClearMap()
    {
        StartCoroutine("ClearMapEnum");
    }

    private IEnumerator ClearMapEnum()
    {
        collider.enabled = true;

        yield return new WaitForSeconds(2f);

        collider.enabled = false;
    }
}
