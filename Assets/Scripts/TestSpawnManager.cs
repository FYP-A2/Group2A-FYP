using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawnManager : MonoBehaviour
{
    public static TestSpawnManager Instance;

    Spawnpoint[] spawnpoints;

    public GameObject monsterPrefab;
    void Awake()
    {
        Instance = this;
        spawnpoints = GetComponentsInChildren<Spawnpoint>();
        InvokeRepeating("SpawnEnemy", 0f, 5f);
    }

    public Transform GetSpawnpoint()
    {
        return spawnpoints[Random.Range(0, spawnpoints.Length)].transform;
    }

    public Transform GetSpawnpoint(Spawnpoint spawn)
    {
        return spawn.transform;
    }

    void SpawnEnemy()
    {
        //Transform spawnpoint = GetSpawnpoint();
        //Instantiate(monsterPrefab, spawnpoint.position, spawnpoint.rotation);
        Instantiate(monsterPrefab, GetSpawnpoint(spawnpoints[0]).position, GetSpawnpoint(spawnpoints[0]).rotation);
        Instantiate(monsterPrefab, GetSpawnpoint(spawnpoints[1]).position, GetSpawnpoint(spawnpoints[1]).rotation);
        Instantiate(monsterPrefab, GetSpawnpoint(spawnpoints[2]).position, GetSpawnpoint(spawnpoints[2]).rotation);
        Instantiate(monsterPrefab, GetSpawnpoint(spawnpoints[3]).position, GetSpawnpoint(spawnpoints[3]).rotation);
    }
}