using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{

    private MeshRenderer _meshRenderer;
    private Quaternion _rotation;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.sortingOrder = 20;
        _rotation = transform.rotation;
    }

    private void Update()
    {
        transform.rotation = _rotation;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.GetComponent<Bullet>();

        if (bullet != null)
        {
            if(Vector3.Distance(transform.position, bullet.transform.position) >= 9f)
            {
                bullet.Pool.ReturnToPool(bullet.gameObject);
            }
        }
    }
}
