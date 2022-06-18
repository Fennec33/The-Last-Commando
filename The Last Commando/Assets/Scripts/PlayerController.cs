using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashTime = 0.5f;
    [SerializeField] private float recoverTime = 2f;
    [SerializeField] private int health = 5;
    [SerializeField] private float dashRechargeTime;
    [SerializeField] private GameObject gameOverScreen;

    private Rigidbody2D _rb;
    private PlayerAnimations _animations;
    private AudioSource _audioSource;
    private HealthBar _healthBar;
    private Camera _camera;

    private Vector2 _movement;
    private Vector2 _mousePosition;
    private Vector2 _lookDirection;
    private Vector2 _dashDirection;
    private float _recoverTimeLapsed = 0;
    private bool _dashing = false;
    private float _dashTimer = 0f;
    private float _dashResetTimer = 0f;
    private int _maxHealth;
    private Vector3 _startPosition;

    public int Health { get { return health; } }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animations = GetComponent<PlayerAnimations>();
        _camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        _healthBar = GameObject.Find("PlayerHealthBar").GetComponent<HealthBar>();

        _audioSource = GetComponent<AudioSource>();
        _healthBar.SetMaxHealth(health);
        _maxHealth = health;
        _startPosition = transform.position;
    }

    void Update()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");

        _mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);

        _recoverTimeLapsed += Time.deltaTime;
        _dashResetTimer += Time.deltaTime;

        if (Input.GetButtonDown("Jump") && _dashResetTimer >= dashRechargeTime)
        {
            Dash();
        }
    }

    private void FixedUpdate()
    {
        if (!_dashing)
        {
            _rb.MovePosition(_rb.position + _movement * moveSpeed * Time.fixedDeltaTime);

            _lookDirection = _mousePosition - _rb.position;
            float angle = Mathf.Atan2(_lookDirection.y, _lookDirection.x) * Mathf.Rad2Deg - 90f;
            _rb.rotation = angle;
        }
        else
        {
            _rb.MovePosition(_rb.position + _dashDirection * dashSpeed * Time.fixedDeltaTime);

            _dashTimer += Time.deltaTime;

            if (_dashTimer >= dashTime)
            {
                _dashTimer = 0f;
                _dashing = false;
            }
        }
    }

    void Dash()
    {
        if (_dashing) { return; }

        if (_movement.magnitude > 0)
        {
            _dashDirection = _movement.normalized;
            _dashing = true;
        }
        else
        {
            _dashDirection = _lookDirection.normalized;
            _dashing = true;
        }

        _dashResetTimer = 0f;
        _animations.AnimateDash(_lookDirection);
    }

    public void TakeDamage(int damage)
    {
        if (_recoverTimeLapsed < recoverTime)
        {
            return;
        }

        health -= damage;
        _healthBar.SetHealth(health);
        _recoverTimeLapsed = 0;
        _animations.StartFlashing(recoverTime);

        if (health <= 0)
        {
            Die();
        }
    }

    public void Heal(int healing)
    {
        health += healing;
        if (health > _maxHealth)
        {
            health = _maxHealth;
        }
        
        _healthBar.SetHealth(health);
    }

    private void Die()
    {
        gameOverScreen.SetActive(true);
        gameObject.SetActive(false);
    }

    public void PlaySound(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }

    public void Respawn()
    {
        gameObject.SetActive(true);

        if (health < _maxHealth)
        {
            transform.position = _startPosition;
        }
        health = _maxHealth;
        _healthBar.SetHealth(health);
    }
}
