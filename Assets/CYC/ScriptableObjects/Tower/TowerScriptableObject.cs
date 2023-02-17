using UnityEngine;

[CreateAssetMenu(fileName = "Tower Configuration", menuName = "ScriptableObjects.Tower Configuration")]
public class TowerScriptableObject : ScriptableObject
{
    public int phyDamage;
    public int magicDamage;
    public int fireRate;
    public int attackRange;
    public string Type;
    public int level = 0;
    public GameObject bulletPrefab;
}
