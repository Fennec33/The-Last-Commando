using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    [SerializeField] private int damage = 3;
    [SerializeField] private float explosionTime = 1f;
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip explosionSound;

    private List<GameObject> _targets = new List<GameObject>();
    private float _timer = 0f;
    private bool _exploded = false;

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= explosionTime && !_exploded)
        {
            GetComponent<CircleCollider2D>().enabled = true;
            particles.Play();
            audioSource.PlayOneShot(explosionSound);
            _exploded = true;
        }
        if (_timer >= explosionTime + 1f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !_targets.Contains(collision.gameObject))
        {
            _targets.Add(collision.gameObject);
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
        }
    }
}
