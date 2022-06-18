using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmerAnimations : MonoBehaviour
{
    [SerializeField] private float animationSpeed;
    [SerializeField] private float legRotation;

    private Transform _legFL;
    private Transform _legFR;
    private Transform _legCL;
    private Transform _legCR;
    private Transform _legBL;
    private Transform _legBR;

    private bool _walkingState = true;

    private void Awake()
    {
        Transform _temp = this.transform.Find("SwarmerEnemyGFX");

        _legFL = _temp.Find("LegFL");
        _legFR = _temp.Find("LegFR");
        _legCL = _temp.Find("LegCL");
        _legCR = _temp.Find("LegCR");
        _legBL = _temp.Find("LegBL");
        _legBR = _temp.Find("LegBR");
    }

    private void FixedUpdate()
    {
        WalkingAnimation();
    }

    private void WalkingAnimation()
    {
        if (_walkingState)
        {
            RotateLegGroup(1, animationSpeed);
            RotateLegGroup(2, -animationSpeed);

            if (_legFL.localRotation.z * Mathf.Rad2Deg >= legRotation)
            {
                _walkingState = false;
            }
        }
        else
        {
            RotateLegGroup(2, animationSpeed);
            RotateLegGroup(1, -animationSpeed);

            if (_legFL.localRotation.z * Mathf.Rad2Deg <= -legRotation)
            {
                _walkingState = true;
            }
        }
    }

    private void RotateLegGroup(int _group, float _rotation)
    {
        switch (_group)
        {
            case 1:
                _legFL.Rotate(0f, 0f, _rotation, Space.Self);
                _legCR.Rotate(0f, 0f, _rotation, Space.Self);
                _legBL.Rotate(0f, 0f, _rotation, Space.Self);
                break;
            case 2:
                _legFR.Rotate(0f, 0f, _rotation, Space.Self);
                _legCL.Rotate(0f, 0f, _rotation, Space.Self);
                _legBR.Rotate(0f, 0f, _rotation, Space.Self);
                break;
        }
    }
}
