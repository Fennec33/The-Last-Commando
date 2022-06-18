using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private float recoilDistance;
    [SerializeField] private float flashingSpeed;

    private Transform _gunArm;
    private Transform _grenadeArm;
    private ParticleSystem _dashParticles;
    private GameObject _playerGFX;

    private Vector3 _gunArmResting;
    private bool _flashing = false;
    private bool _visible = true;
    private float _flashingTime;
    private float _flashTimer = 0f;
    private float _flashingSpeedTimer = 0f;

    private void Awake()
    {
        Transform _temp = this.transform.Find("PlayerGFX");
        _gunArm = _temp.Find("SoldierGun");
        _grenadeArm = _temp.Find("SoldierArmL");
        _playerGFX = _temp.gameObject;

        _dashParticles = this.transform.Find("DashParticle").GetComponent<ParticleSystem>();

        _gunArmResting = _gunArm.localPosition;
    }

    private void Update()
    {
        RecoverRecoil();

        if (_flashing)
        {
            Flashing();
        }
    }

    public void AnimateRecoil()
    {
        Vector3 pos = _gunArmResting;
        pos.y -= recoilDistance;
        _gunArm.localPosition = pos;
    }

    private void RecoverRecoil()
    {
        Vector3 pos = _gunArm.localPosition;
        if (pos.y < _gunArmResting.y)
        {
            pos.y += 0.05f;
            _gunArm.localPosition = pos;
        }
    }

    public void AnimateDash(Vector2 _lookDirection)
    {
        float angle = Mathf.Atan2(_lookDirection.x, _lookDirection.y);
        _dashParticles.startRotation = angle;
        _dashParticles.Play();
    }

    public void StartFlashing(float time)
    {
        _flashingTime = time;
        _flashing = true;
    }

    private void Flashing()
    {
        _flashTimer += Time.deltaTime;
        _flashingSpeedTimer += Time.deltaTime;

        if (_flashingSpeedTimer >= flashingSpeed)
        {
            if (_visible) { _playerGFX.SetActive(false);  _visible = false; }
            else { _playerGFX.SetActive(true); _visible = true; }
            _flashingSpeedTimer = 0f;
        }

        if (_flashTimer >= _flashingTime)
        {
            _flashTimer = 0;
            _flashingSpeedTimer = 0f;
            _playerGFX.SetActive(true);
            _visible = true;
            _flashing = false;
        }
    }
}
