using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Tower Configuration", menuName = "ScriptableObjects.Tower Configuration")]
public class TowerScriptableObject : ScriptableObject
{
    public int damage;
    public int fireRate;
    public float attackRange;
    public string Type;
    public GameObject bulletPrefab;
}