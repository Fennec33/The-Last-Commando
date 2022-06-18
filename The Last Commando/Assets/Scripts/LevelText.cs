using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelText;
    
    private RectTransform _rectTransform;
    private Vector3 _pos;
    private Vector3 _startPos;
    private float _target;

    private void Awake()
    {
        _rectTransform = this.GetComponent<RectTransform>();
        _target = _rectTransform.position.x;
        _startPos = new Vector3(_rectTransform.position.x + 1000, _rectTransform.position.y);
        levelText.enabled = false;
    }

    public void PlayLevelText(int level)
    {
        levelText.text = "Level " + level.ToString();
        _rectTransform.SetPositionAndRotation(_startPos, Quaternion.identity);
        _pos = _startPos;
        levelText.enabled = true;
        StartCoroutine("MoveText");
    }

    private IEnumerator MoveText()
    {
        while (true)
        {
            if (_pos.x - _target >= 40f)
            {
                _pos.x += (_target - _pos.x) * 2f * Time.deltaTime;
            }
            else if (_pos.x - _target <= -1000f)
            {
                levelText.enabled = false;
                yield break;
            }
            else if (_pos.x - _target <= -15f)
            {
                _pos.x += (_target - _pos.x) * -3f * Time.deltaTime - 8f;
            }
            else
            {
                _pos.x += -50f * Time.deltaTime;
            }

            _rectTransform.SetPositionAndRotation(_pos, Quaternion.identity);

            yield return new WaitForEndOfFrame();
        }
    }   
}
