using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private LevelManager levelManager;

    [SerializeField] private Wave[] waves;
    [SerializeField] private float[] waveSpawnTime;
    [SerializeField] private bool spawnHealthPack;
    [SerializeField] private bool spawnGrenadePack;

    private float _timer = 0f;
    private int _counter = 0;
    private int _numbrOfWaves;

    public bool SpawnHealthPack
    {
        get { return spawnHealthPack; }
    }

    public bool SpawnGrenadePack
    {
        get { return spawnGrenadePack; }
    }

    private void Start()
    {
        _numbrOfWaves = waves.Length;
    }

    private void OnEnable()
    {
        _timer = 0f;
        _counter = 0;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= waveSpawnTime[_counter])
        {
            waveManager.BuildWave(waves[_counter]);

            _counter += 1;
            _timer = 0f;
        }

        if (_counter >= _numbrOfWaves)
        {
            levelManager.LevelOver();
            enabled = false;
        }
    }

}
