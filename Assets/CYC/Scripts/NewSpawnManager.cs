using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class EnemySpawnData
{
    public GameObject prefab;
    public List<float> spawnDelays;
    public List<int> numToSpawn;
}

public class NewSpawnManager : MonoBehaviour
{
    public static NewSpawnManager Instance;

    public float range = 5.0f;
    public List<Transform> spawnPoints;
    public List<EnemySpawnData> enemySpawnData;
    public float stageDuration = 180.0f;
    private int currentStage = 0;
    public List<EnemyScriptableObject> enemyScriptableObjects;
    bool spawned = false;
    private void Awake()
    {
        Instance = this;
        spawnPoints.AddRange(GetComponentsInChildren<Transform>());
        spawnPoints.Remove(transform);
        //StartCoroutine(SpawnPrefabs());
        //StartCoroutine(NewSpawnPrefabs(5));
    }

    private IEnumerator SpawnPrefabs()
    {
        if (spawned) { yield return null; }
        else
        {
            this.spawned = true;
            while (true)
            {
                float stageStartTime = Time.time;
                float stageEndTime = stageStartTime + stageDuration;
                currentStage++;
                if (currentStage > 1)
                {
                    foreach (EnemyScriptableObject e in enemyScriptableObjects)
                    {
                        //e.hp = (int)(e.hp*1.1);//work
                    }
                }

                Debug.Log("Starting stage " + currentStage);

                foreach (EnemySpawnData data in enemySpawnData)
                {
                    if (data.numToSpawn[currentStage - 1] > 0)
                    {
                        int numToSpawn = data.numToSpawn[currentStage - 1];
                        float totalSpawnTime = stageDuration;
                        float spawnRate = totalSpawnTime / numToSpawn;

                        int numSpawned = 0;
                        while (numSpawned < numToSpawn)
                        {
                            int numToBurst = Random.Range(2, 6);
                            float burstDelay = Random.Range(0.1f, 1.0f);
                            yield return new WaitForSeconds(burstDelay);

                            Vector3 spawnPoint = GetSpawnpoint().position;

                            for (int i = 0; i < numToBurst && numSpawned < numToSpawn; i++)
                            {
                                if (RandomPoint(spawnPoint, range, out Vector3 point))
                                {
                                    Instantiate(data.prefab, point, Quaternion.identity);
                                    numSpawned++;
                                }
                            }

                            if (numSpawned < numToSpawn)
                            {
                                float spawnDelay = Random.Range(spawnRate / 2, spawnRate);
                                yield return new WaitForSeconds(spawnDelay);
                            }
                        }
                    }
                }

                while (Time.time < stageEndTime)
                {
                    yield return null;                    
                }

            }
        }
    }

    private Transform GetSpawnpoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Count)];
    }

    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, range, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }
        result = Vector3.zero;
        return false;
    }

    public IEnumerator NewSpawnPrefabs(int level)
    {
            Debug.Log("Starting stage " + level);

            foreach (EnemySpawnData data in enemySpawnData)
            {
                if (data.numToSpawn[level - 1] > 0)
                {
                    int numToSpawn = data.numToSpawn[level - 1];
                    float totalSpawnTime = stageDuration;
                    float spawnRate = totalSpawnTime / numToSpawn;

                    int numSpawned = 0;
                    while (numSpawned < numToSpawn)
                    {
                        int numToBurst = Random.Range(2, 6);
                        float burstDelay = 1f;
                        yield return new WaitForSeconds(burstDelay);

                        Vector3 spawnPoint = GetSpawnpoint().position;

                        for (int i = 0; i < numToBurst && numSpawned < numToSpawn; i++)
                        {
                            if (RandomPoint(spawnPoint, range, out Vector3 point))
                            {
                                Instantiate(data.prefab, point, Quaternion.identity);
                                numSpawned++;
                            }
                        }

                        if (numSpawned < numToSpawn)
                        {
                            float spawnDelay = spawnRate;
                            yield return new WaitForSeconds(spawnDelay);
                        }
                    }
                }
            }
            Debug.Log("End " + level);       
    }  
}