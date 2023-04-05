using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class EnemyData
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
    public List<EnemyData> enemySpawnData;
    public List<EnemyScriptableObject> enemyScriptableObjects;
    private void Awake()
    {
        Instance = this;
        spawnPoints.AddRange(GetComponentsInChildren<Transform>());
        spawnPoints.Remove(transform);
        //StartCoroutine(SpawnPrefabs());
        //StartCoroutine(NewSpawnPrefabs(5));
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

    public IEnumerator NewSpawnPrefabs(EnemyData enemyData, Stage data)
    {
        Debug.Log("Starting stage " + data.currentStage);

        if (enemyData.numToSpawn[data.currentStage - 1] > 0)
        {
            int numToSpawn = enemyData.numToSpawn[data.currentStage - 1];
            //float spawnRate = data.stageDuration / numToSpawn;

            int numSpawned = 0;
            while (numSpawned < numToSpawn)
            {
                int numToBurst = Random.Range(2, 6);
                //float burstDelay = 1f;
                //yield return new WaitForSeconds(burstDelay);

                Vector3 spawnPoint = GetSpawnpoint().position;

                for (int i = 0; i < numToBurst && numSpawned < numToSpawn; i++)
                {
                    if (RandomPoint(spawnPoint, range, out Vector3 point))
                    {
                        Instantiate(enemyData.prefab, point, Quaternion.identity);
                        numSpawned++;
                    }
                }

                if (numSpawned < numToSpawn)
                {
                    //float spawnDelay = spawnRate;
                    yield return new WaitForSeconds(2f);
                }
            }
        }
        Debug.Log("End " + data.currentStage);

    }  
}