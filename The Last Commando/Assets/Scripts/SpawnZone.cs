using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZone : MonoBehaviour
{
    [SerializeField] GameObject [] spawnPoints;

    public void Spawn(GameObject[] objects)
    {
        for (int i = 0; i < objects.Length && i < spawnPoints.Length; i++)
        {
            objects[i].transform.position = spawnPoints[i].transform.position;
            objects[i].SetActive(true);
        }
    }
}
