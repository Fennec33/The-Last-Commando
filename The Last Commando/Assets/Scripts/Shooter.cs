using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shooter : MonoBehaviour
{
    [SerializeField] private float bulletForce = 20f;
    [SerializeField] private float fireRate = 0.1f;
    [SerializeField] private float inacuracy = 0.1f;
    [SerializeField] private GameObject grenadePrefab;
    [SerializeField] private float grenadeForce = 10f;
    [SerializeField] private int grenadeCount = 3;

    [SerializeField] GameObjectPool gameObjectPool;

    private Transform _firePoint;
    private Transform _grenadePoint;
    private TextMeshProUGUI _grenadeText;
    private PlayerAnimations _animations;

    private Vector2 _shootDirection;
    private float _fireTimer;
    private int _startGrenadeCount;

    private void Awake()
    {
        _firePoint = transform.Find("FirePoint");
        _grenadePoint = transform.Find("GrenadePoint");
        _animations = GetComponent<PlayerAnimations>();

        _grenadeText = GameObject.Find("UICanvas/GrenadeIcon/Text (TMP)").GetComponent<TextMeshProUGUI>();

        _fireTimer = fireRate;
        _grenadeText.text = grenadeCount.ToString();
        _startGrenadeCount = grenadeCount;
    }

    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Shoot();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            ThrowGrenade();
        }
    }

    void Shoot()
    {
        _fireTimer += Time.deltaTime;

        if (_fireTimer >= fireRate)
        {
            var bullet = gameObjectPool.Get();
            bullet.transform.position = _firePoint.position;
            bullet.transform.rotation = _firePoint.rotation;
            _shootDirection = AddInacuracy(_firePoint.up);

            bullet.SetActive(true);
            bullet.GetComponent<Rigidbody2D>().AddForce(_shootDirection * bulletForce, ForceMode2D.Impulse);
            _fireTimer = 0f;

            _animations.AnimateRecoil();
        }
    }

    public void ThrowGrenade()
    {
        if (grenadeCount <= 0) { return; }
        grenadeCount -= 1;
        _grenadeText.text = grenadeCount.ToString();

        GameObject _Grenade = Instantiate(grenadePrefab, _grenadePoint.position, _grenadePoint.rotation);
        Rigidbody2D _rb = _Grenade.GetComponent<Rigidbody2D>();
        _rb.AddForce(_grenadePoint.up * grenadeForce, ForceMode2D.Impulse);
    }

    public void AddGrenades(int num)
    {
        grenadeCount += num;
        if (grenadeCount < 0)
        {
            grenadeCount = 0;
        }
        _grenadeText.text = grenadeCount.ToString();
    }

    private Vector2 AddInacuracy(Vector2 vector)
    {
        vector.x = vector.x + Random.value * inacuracy - inacuracy / 2;
        vector.y = vector.y + Random.value * inacuracy - inacuracy / 2;

        return vector;
    }
    public void Respawn()
    {
        grenadeCount = _startGrenadeCount;
        _grenadeText.text = grenadeCount.ToString();
    }

}
