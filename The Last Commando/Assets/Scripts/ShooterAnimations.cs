using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterAnimations : MonoBehaviour
{
    [SerializeField] private float recoilDistance;
    [SerializeField] private float recoilRecovery;
    
    private Transform _leftGun;
    private Transform _rightGun;
    private Vector3 _leftGunResting;
    private Vector3 _rightGunResting;
    private Vector3 _leftRecoilPos;
    private Vector3 _rightRecoilPos;

    private void Awake()
    {
        Transform _temp = this.transform.Find("ShooterEnemyGFX");
        _leftGun = _temp.Find("ArmL");
        _rightGun = _temp.Find("ArmR");

        _leftGunResting = _leftGun.localPosition;
        _rightGunResting = _rightGun.localPosition;

        _leftRecoilPos = _leftGunResting;
        _leftRecoilPos.y -= recoilDistance;
        _rightRecoilPos = _rightGunResting;
        _rightRecoilPos.y -= recoilDistance;
    }

    private void Update()
    {
        RecoverRecoil();
    }

    public void AnimateRecoil(int _gun)
    {
        if (_gun == 1)
        {
            _leftGun.localPosition = _leftRecoilPos;
        }
        else
        {
            _rightGun.localPosition = _rightRecoilPos;
        }
    }

    private void RecoverRecoil()
    {
        Vector3 pos = _rightGun.localPosition;
        if (pos.y < _rightGunResting.y)
        {
            pos.y += recoilRecovery;
            _rightGun.localPosition = pos;
        }

        pos = _leftGun.localPosition;
        if (pos.y < _leftGunResting.y)
        {
            pos.y += recoilRecovery;
            _leftGun.localPosition = pos;
        }
    }
}
