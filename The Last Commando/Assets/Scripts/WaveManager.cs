using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private GameObject swarmerEnemy;
    [SerializeField] private GameObject shooterEnemy;
    [SerializeField] private GameObject shieldEnemy;
    [SerializeField] private GameObject artilleryEnemy;

    [SerializeField] private SpawnZone[] spawnZone;

    [SerializeField] private GameObjectPool swarmerEnemyPool;
    [SerializeField] private GameObjectPool shooterEnemyPool;
    [SerializeField] private GameObjectPool shieldEnemyPool;
    [SerializeField] private GameObjectPool artilleryEnemyPool;

    public GameObjectPool SwarmerEnemyPool { get { return swarmerEnemyPool; } }
    public GameObjectPool ShooterEnemyPool { get { return shooterEnemyPool; } }
    public GameObjectPool ShieldEnemyPool { get { return shieldEnemyPool; } }
    public GameObjectPool ArtilleryEnemyPool { get { return artilleryEnemyPool; } }

    public void BuildWave(Wave waveMap)
    {
        for (int i = 0; i < waveMap.NumberOfZones(); i++)
        {
            spawnZone[i].Spawn( PopulateZone(waveMap, i) );
        }
    }

    private GameObject[] PopulateZone(Wave waveMap, int zone)
    {
        GameObject[] wave = new GameObject[waveMap.GetTotalEnemysInZone(zone)];

        int i = 0;
        int j = 0;
        for (i = 0; i < waveMap.swarmerEnemyAmount[zone] && i + j < wave.Length; i++)
        {
            wave[i + j] = swarmerEnemyPool.Get();
        }
        j += i;
        for (i = 0; i < waveMap.shooterEnemyAmount[zone] && i + j < wave.Length; i++)
        {
            wave[i + j] = shooterEnemyPool.Get();
        }
        j += i;
        for (i = 0; i < waveMap.shieldEnemyAmount[zone] && i + j < wave.Length; i++)
        {
            wave[i + j] = shieldEnemyPool.Get();
        }
        j += i; for (i = 0; i < waveMap.artilleryEnemyAmount[zone] && i + j < wave.Length; i++)
        {
            wave[i + j] = artilleryEnemyPool.Get();
        }

        return wave;
    }

    public bool IsMapCleared()
    {
        bool answer = true;

        if (!swarmerEnemyPool.AllObjectsInPool())
        {
            answer = false;
        }
        if (!shooterEnemyPool.AllObjectsInPool())
        {
            answer = false;
        }
        if (!shieldEnemyPool.AllObjectsInPool())
        {
            answer = false;
        }
        if (!artilleryEnemyPool.AllObjectsInPool())
        {
            answer = false;
        }

        return answer;
    }
}
