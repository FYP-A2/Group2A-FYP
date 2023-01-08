using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    Vector3 randomPos, min, max;
    [SerializeField] Transform player;
    [SerializeField] GameObject monsterPrefab;
    [SerializeField] GameObject miniBossPrefab;
    [SerializeField] GameObject bossPrefab;
    int monsterCount, bossCount;
    public int count;
    float spawnDelay;
    bool spawned, bossSpawned, minibossSpawned;
    [SerializeField] Terrain terrain;
    public float gameTime, boostCount, boost = 1.1f;
    public bool start;
    RaycastHit hit;

    private void Start()
    {
        min = terrain.terrainData.bounds.min + terrain.GetPosition();
        max = terrain.terrainData.bounds.max + terrain.GetPosition();
        start = false;
        bossCount = 0;
    }
    private void Update()
    {
        

        if (start)
        {
            gameTime += Time.deltaTime;
            if (gameTime < 300)
            {
                if (!spawned)
                {
                    Spawn(monsterPrefab, 0);
                    count++;
                }
            }
            else
            {
                if (!spawned)
                {
                    if (Random.Range(0, 100) < 50)
                    {
                        Spawn(miniBossPrefab, 0);
                        count++;
                        Debug.Log(count);
                    }
                    else
                    {
                        Spawn(monsterPrefab, 0);
                        count++;
                        Debug.Log(count);
                    }
                }
            }

            if (gameTime > 180 && gameTime < 185)
            {
                if (!minibossSpawned)
                {
                    Spawn(miniBossPrefab, 1);
                }
            }

            if (gameTime > 300 && gameTime < 301f)
            {
                if (!bossSpawned)
                {
                    randomPos = new Vector3(Random.Range(player.position.x - 50, player.position.x + 50), max.y, Random.Range(player.position.z - 50, player.position.z + 50));
                    if (Physics.Linecast(randomPos, new Vector3(randomPos.x, -1f, randomPos.z), out hit))
                    {
                        Debug.Log("BossWork" + gameTime);
                        if (hit.collider.name.Equals("terrain"))
                        {
                            bossPrefab.transform.position = hit.point;
                            bossPrefab.SetActive(true);
                            bossSpawned = true;
                            gameTime = 0;
                            bossSpawned = false;
                            minibossSpawned = false;
                            boostCount++;
                            boost *= boost;
                        }
                    }
                }
            }

        }
    }
    //public void SpawnEnemy(int monsterNo, int bossNo)
    //{
    //    for (int i = 0; i < monsterNo; i++)
    //    {
    //        Spawn(monsterPrefab);
    //    }

    //    for (int i = 0; i < bossNo; i++)
    //    {
    //        Spawn(miniBossPrefab);
    //    }
    //}

    void Spawn(GameObject prefab, int mode)
    {
        //randomPos = new Vector3(Random.Range(500f, 600f), 45f, Random.Range(300f, 400f));
        //randomPos = new Vector3(Random.Range(min.x, max.x), max.y, Random.Range(min.z, max.z));
        randomPos = new Vector3(Random.Range(player.position.x - 100, player.position.x + 100), max.y, Random.Range(player.position.z - 100, player.position.z + 100));
        if (Physics.Linecast(randomPos, new Vector3(randomPos.x, -1f, randomPos.z), out hit))
        {

            if (hit.collider.name.Equals("terrain") && Vector3.Distance(player.position, hit.point) > 50)
            {
                Debug.Log("spawnWork" + gameTime);
                Instantiate(prefab, hit.point, Quaternion.identity);
                if (mode == 1)
                {
                    minibossSpawned = true;
                }
                else
                    spawned = true;
                Invoke("ResetSpawn", Random.Range(5, 10));
            }
        }
    }

    void ResetSpawn()
    {
        spawned = false;
    }
}