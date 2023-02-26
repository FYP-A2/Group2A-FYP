using UnityEngine;
using UnityEngine.AI;

public class Spawn : MonoBehaviour
{
    public float range = 5.0f;
    public GameObject prefab;

    private void Start()
    {
        InvokeRepeating("SpawnEnemy", 0, 1);
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
    void SpawnEnemy()
    {
        Vector3 point;
        if (RandomPoint(transform.position, range, out point))
        {
            Debug.DrawRay(point, Vector3.up, Color.blue, 5.0f);
            Instantiate(prefab, point, Quaternion.identity);
        }
    }
}