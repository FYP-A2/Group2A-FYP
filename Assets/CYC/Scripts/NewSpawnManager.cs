using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class EnemyPath
{
    public List<EnemyPathLists> Spawn;
    public List<SpawnLists> isSpawn;
}

[System.Serializable]
public class EnemyPathLists
{
    [NonReorderable] public List<Transform> path1;
    [NonReorderable] public List<Transform> path2;
    [NonReorderable] public List<Transform> path3;
}

[System.Serializable]
public class SpawnLists
{
    [NonReorderable] public List<bool> spawn;
}

public class NewSpawnManager : MonoBehaviour
{
    public static NewSpawnManager Instance;

    public float range = 5.0f;
    public List<Transform> spawnPoints;
    public List<MonsterDictionary> enemySpawnData;
    public List<EnemyScriptableObject> enemyScriptableObjects;
    public EnemyPath enemyPath;
    public int pathIndex;
    public List<List<Transform>> currentSpawnersPath = new List<List<Transform>>();
    public GameObject lineRenderer;
    public List<LineRenderer> lineRenderers= new List<LineRenderer>();
    //public Stage stage;
    private void Awake()
    {
        Instance = this;
        spawnPoints.AddRange(GetComponentsInChildren<Transform>());
        spawnPoints.Remove(transform);
        //StartCoroutine(SpawnPrefabs());
        //StartCoroutine(NewSpawnPrefabs(5));
        //for (int i = 0; i < enemySpawnData.Count; i++)
        //{
        //    if (enemySpawnData[i].prefab == null)
        //        enemySpawnData[i].SetMonsterDictionary(enemySpawnData[i].type);
        //}
        //foreach (MonsterDictionary e in enemySpawnData)
        //{            
        //    StartCoroutine(NewSpawnPrefabs(e, stage));
        //}
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            GameObject l = Instantiate(lineRenderer, Vector3.zero, Quaternion.identity, null);
            lineRenderers.Add(l.GetComponent<LineRenderer>());
            lineRenderers[i].enabled = false;
        }
    }

    public void PathDisplay(int stageIndex)
    {
        if(currentSpawnersPath.Count != 0)
        {
            currentSpawnersPath = new List<List<Transform>>();
        }
        for (int i = 0; i < spawnPoints.Count; i++)
        {           
            currentSpawnersPath.Add(GetRandomPath(i));
            lineRenderers[i].positionCount = currentSpawnersPath[i].Count;
            lineRenderers[i].SetPositions(currentSpawnersPath[i].Select(t => t.position).ToArray());
            //Debug.Log(enemyPath.isSpawn[stageIndex].spawn[i]);
            if (enemyPath.isSpawn[stageIndex].spawn[i])
            {
                lineRenderers[i].enabled = true;
            }
            else
            {
                lineRenderers[i].enabled = false;
            }
        }
    }
   
    private Transform GetSpawnpoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Count)];
    }

    private Vector3 GetSpawnpoint(int index)
    {
        return spawnPoints[index].position;
    }

    private List<Transform> GetRandomPath(int index)
    {
        EnemyPathLists randomPath = enemyPath.Spawn[index];
        List<Transform> chosenPath;
        int randomPathIndex = Random.Range(0, 2);
        switch (randomPathIndex)
        {
            case 0:
                chosenPath = randomPath.path1;
                return chosenPath;
            case 1:
                chosenPath = randomPath.path2;
                return chosenPath;
            case 2:
                chosenPath = randomPath.path3;
                return chosenPath;
        }
        return null;
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

    public IEnumerator NewSpawnPrefabs(MonsterDictionary enemyData, Stage data)
    {
        //Debug.Log("Starting stage " + data.currentStage);
        if (enemyData.numToSpawn_WhereEachItemMeans_In_A_Level[data.currentStage - 1] > 0)
        {
            int numToSpawn = enemyData.numToSpawn_WhereEachItemMeans_In_A_Level[data.currentStage - 1];
            //float spawnRate = data.stageDuration / numToSpawn;

            int numSpawned = 0;
            while (numSpawned < numToSpawn)
            {
                int numToBurst = Random.Range(2, 6);
                //float burstDelay = 1f;
                //yield return new WaitForSeconds(burstDelay);

                //Vector3 spawnPoint = GetSpawnpoint().position;
                int spawnPointIndex;
                if (enemyData.whichSpawn != null)
                {
                    spawnPointIndex = enemyData.whichSpawn[data.currentStage - 1]-1;
                }
                else 
                {
                    spawnPointIndex = Random.Range(0, spawnPoints.Count);
                }

                for (int i = 0; i < numToBurst && numSpawned < numToSpawn; i++)
                {
                    //if (RandomPoint(spawnPoint, range, out Vector3 point))
                    if(RandomPoint(GetSpawnpoint(spawnPointIndex),range,out Vector3 point))
                    {
                        GameObject enemy = Instantiate(enemyData.prefab, point, Quaternion.identity);
                        for(int j=0; j < spawnPoints.Count;j++)
                        {
                            if(spawnPointIndex == j)
                            {
                                if (!enemy.GetComponent<StageMonster>().wayPointMode)
                                    enemy.GetComponent<StageMonster>().wayPointMode = true;
                                enemy.GetComponent<StageMonster>().waypoints = currentSpawnersPath[j].GetRange(1, currentSpawnersPath[j].Count-1);
                            }
                        }
                        /*switch (spawnPointIndex){
                            case 0:
                                if (!enemy.GetComponent<StageMonster>().wayPointMode)
                                    enemy.GetComponent<StageMonster>().wayPointMode = true;
                                enemy.GetComponent<StageMonster>().waypoints = enemyPath.Sapwn[0].path1;
                                break;
                            case 1:
                                if (!enemy.GetComponent<StageMonster>().wayPointMode)
                                    enemy.GetComponent<StageMonster>().wayPointMode = true;
                                enemy.GetComponent<StageMonster>().waypoints = enemyPath.Sapwn[1].path1;
                                break;
                            case 2:
                                if (!enemy.GetComponent<StageMonster>().wayPointMode)
                                    enemy.GetComponent<StageMonster>().wayPointMode = true;
                                enemy.GetComponent<StageMonster>().waypoints = enemyPath.Sapwn[2].path1;
                                break;
                        }*/                  
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
        //Debug.Log("End " + data.currentStage);

    }  
}