using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Unity.Netcode;

public class SpawnManager : NetworkBehaviour
{
    public static SpawnManager Instance;

    public float range = 5.0f, SpawnDelay;
    public int numToSpawn;
    [SerializeField] List<Transform> spawnPoints;
    [SerializeField] List<GameObject> prefabs;

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            Instance = this;
            spawnPoints.AddRange(GetComponentsInChildren<Transform>());
            spawnPoints.Remove(transform);
            StartCoroutine(SpawnEnemy(SpawnDelay, numToSpawn));
        }
    }
    
    public IEnumerator SpawnEnemy(float delay, int numToSpawn)
    {
        WaitForSeconds Wait = new WaitForSeconds(SpawnDelay);
        int Spawned = 0;
        Vector3 point;
        while (Spawned < numToSpawn) {
            if (RandomPoint(GetSpawnpoint().position, range, out point))
            {
                int SpawnIndex = Spawned % prefabs.Count;
                //Debug.DrawRay(point, Vector3.up, Color.blue, 5.0f);
                Transform m = Instantiate(prefabs[SpawnIndex], point, Quaternion.identity).transform;
                m.GetComponent<NetworkObject>().Spawn(true);
                Spawned++;
                yield return Wait;
            }
        }
    }

    Transform GetSpawnpoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Count)];
    }

    Transform GetSpawnpoint(Transform spawn)
    {
        return spawn;
    }
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
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
}