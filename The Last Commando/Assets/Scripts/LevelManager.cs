using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private LevelText levelText;
    [SerializeField] private Level[] levels;

    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private TextMeshProUGUI gameOverText;

    [SerializeField] private Vector3 healthPackSpawnPoint;
    [SerializeField] private Vector3 grenadePackSpawnPoint;
    [SerializeField] private GameObjectPool healthPackPool;
    [SerializeField] private GameObjectPool grenadePackPool;

    private bool _levelOver = true;
    private int _currentLevel = 0;

    public void StartGame()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].enabled = false;
        }

        _levelOver = true;
        _currentLevel = 0;
        StartCoroutine("CheckLevelStatus");
    }

    private IEnumerator CheckLevelStatus()
    {
        while (true)
        {
            if (_levelOver == true && waveManager.IsMapCleared())
            {
                if (_currentLevel >= levels.Length)
                {
                    Debug.Log("Game Won");
                    WinGame();
                    StopCoroutine("CheckLevelStatus");
                }
                else
                {
                    levelText.PlayLevelText(_currentLevel + 1);
                    levels[_currentLevel].enabled = true;
                    _levelOver = false;

                    if (levels[_currentLevel].SpawnHealthPack == true)
                    {
                        PlaceHealthPack();
                    }
                    if (levels[_currentLevel].SpawnGrenadePack == true)
                    {
                        PlaceGrenadePack();
                    }
                }
            }

            yield return new WaitForSeconds(3f);
        }
    }

    private void PlaceHealthPack()
    {
        GameObject healthPack = healthPackPool.Get();
        healthPack.transform.position = healthPackSpawnPoint;
        healthPack.SetActive(true);
    }

    private void PlaceGrenadePack()
    {
        GameObject grenadePack = grenadePackPool.Get();
        grenadePack.transform.position = grenadePackSpawnPoint;
        grenadePack.SetActive(true);
    }

    public void LevelOver()
    {
        _currentLevel++;
        _levelOver = true;
    }

    private void WinGame()
    {
        gameOverScreen.SetActive(true);
        gameOverText.text = " You Won!";
    }
}
