using UnityEngine;

[CreateAssetMenu(fileName = "Tower Configuration", menuName = "ScriptableObjects.Tower Configuration")]
public class TowerScriptableObject : ScriptableObject
{
    [Header("basic")]
    public GameObject towerPrefab;
    public int phyDamage = 20;
    public int magicDamage = 0;
    public int fireRate = 2;
    public int attackRange = 10;
    public TowerType towerType;
    public int level = 0;
    public GameObject bulletPrefab;
    [Header("Fire")]
    public int burntDamage = 0;
    public float burntTime = 0;
    public float exposionRadius = 0;
    [Header("Ice")]
    [Range(0, 1)] public float slowRatio = 0;
    public float slowTime = 0;
    [Header("Toxic")]
    public float penetrationRatio = 0;
    public float penetrationTime = 0;
    [Header("Electro")]
    public int jumpCount = 0;   

    public enum TowerType
    {
        Phy,
        Fire,
        Ice,
        Toxic,
        Electro,
        Magic
    }
}
