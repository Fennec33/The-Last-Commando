using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] private int damage = 3;
    [SerializeField] private float explosionRadius = 3f;
    [SerializeField] private float explosionTime = 2.5f;
    [SerializeField] private ParticleSystem particals;
    [SerializeField] private AudioClip explosionSound;
    [SerializeField] private GameObject gfx;
    [SerializeField] private float explosionDiration = 0.5f;

    private CircleCollider2D _circleColliuder2D;
    private List<GameObject> _targets = new List<GameObject>();
    private AudioSource _audioSource;

    private float _timeLive = 0f;
    private bool _isExploded = false;

    private void Awake()
    {
        _circleColliuder2D = GetComponent<CircleCollider2D>();
        _audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        _timeLive += Time.deltaTime;

        if (_timeLive >= explosionTime && !_isExploded)
        {
            _isExploded = true;
            
            _circleColliuder2D.isTrigger = true;
            _circleColliuder2D.radius = explosionRadius;

            particals.Play();
            _audioSource.PlayOneShot(explosionSound);

            gfx.SetActive(false);
        }

        if (_timeLive >= explosionTime + explosionDiration)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && !_targets.Contains(collision.gameObject))
        {
            _targets.Add(collision.gameObject);
            collision.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
        }
    }
}
