using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Enemy Configuration", menuName = "ScriptableObjects.Enemy Configuration")]
public class EnemyScriptableObject : ScriptableObject
{
    [Header("Attr")]
    public int hp = 100;
    public int damage = 10;
    public float defense = 0;
    public float resistance = 0;
    public float attackRange;
    public float attackDelay;
    public bool isRanged = false;
    public GameObject bullet;  

    [Header("NavMeshAgent")]
    public float baseOffset = 1;
    public float speed = 3f;
    public float angularSpeed = 120;
    public float acceleration = 8;
    public float stoppingDistance = 0.5f;
    public float radius = 0.5f;
    public float height = 2f;
    public ObstacleAvoidanceType obstacleAvoidance = ObstacleAvoidanceType.LowQualityObstacleAvoidance;
    public int avoidancePriority = 50;
    public int areaMask = -1;
    public Vector3 velocity = Vector3.one;
}
