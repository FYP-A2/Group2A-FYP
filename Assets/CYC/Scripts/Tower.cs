using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] TowerScriptableObject towerSO;
    List<TowerScriptableObject> allTowerSO = new List<TowerScriptableObject>();
    List<Transform> monsters;
    public SphereCollider sphereCollider;
    public Transform firePoint;
    bool isAttacked;
    public LayerMask layer;
    public NavMeshModifierVolume modifierVolume;
    int phyDamage, magicDamage, attackRange, fireRate;
    float upgradeAOE;
    void Start()
    {
        allTowerSO.Add(towerSO);
        sphereCollider.radius = towerSO.attackRange;
        monsters = sphereCollider.transform.GetComponent<AttackArea>().targets;
        if(modifierVolume != null)
        {
            modifierVolume.size = new Vector3(towerSO.attackRange*2, towerSO.attackRange*2, towerSO.attackRange*2);
        }
        phyDamage = towerSO.phyDamage;
        magicDamage = towerSO.magicDamage;
        attackRange = towerSO.attackRange;
        fireRate = towerSO.fireRate;
        if (firePoint == null)
            firePoint = transform;
        upgradeAOE = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (monsters.Count>0)
        {
            if (towerSO.towerType.ToString() == "Toxic")
            {
                foreach (Transform t in monsters) {
                    if (t != null)
                    {
                        IMonster m;
                        t.TryGetComponent<IMonster>(out m);
                        if (m != null)
                            m.DefenseReduction(towerSO.penetrationRatio, towerSO.penetrationTime);
                    }
                }
            }
            else if(towerSO.towerType.ToString() == "Ice")
            {
                if (!isAttacked)
                {
                    for (int i = 0; i < monsters.Count; i++)
                    {
                        if (monsters[i] == null) return;
                        RaycastHit hit;
                        Physics.Raycast(firePoint.position, (monsters[i].position - firePoint.position).normalized, out hit, attackRange, layer);
                        if (hit.transform != null && hit.transform.tag == "Monster" && !hit.transform.GetComponent<Monster>().isSlow)
                        {
                            isAttacked = true;
                            Shoot(monsters[i]);
                            return;
                        }
                    }
                    if (!isAttacked)
                    {
                        RaycastHit hit;
                        Physics.Raycast(firePoint.position, (monsters[0].position - firePoint.position).normalized, out hit, attackRange, layer);
                        if (hit.transform != null && hit.transform.tag == "Monster")
                        {
                            isAttacked = true;
                            Shoot(monsters[0]);
                        }
                    }
                }
            }
            else if (towerSO.towerType.ToString() == "Electro")
            {
                if (!isAttacked)
                {
                    int i = 0;
                    if(i < towerSO.jumpCount)
                    {
                        
                        if (monsters.Count > 1)
                        {
                            List<GameObject> bulletGO = new List<GameObject>();
                            bulletGO.Add(Instantiate(towerSO.bulletPrefab, firePoint.position,firePoint.rotation));
                            TestVFX bullet;
                            bulletGO[0].TryGetComponent<TestVFX>(out bullet);
                            if (bullet != null)
                            {
                                bullet.SetPos(firePoint.gameObject, monsters[0].gameObject);
                                monsters[0].GetComponent<Monster>().TakeDamage(phyDamage, magicDamage);
                            }
                            i++;
                            for (int j = 1; j < monsters.Count; j++)
                            {
                                bulletGO.Add(Instantiate(towerSO.bulletPrefab, monsters[j].position, monsters[j].rotation));
                                bulletGO[j].TryGetComponent<TestVFX>(out bullet);
                                bullet.SetPos(monsters[i-1].gameObject, monsters[i].gameObject);
                                monsters[j].GetComponent<Monster>().TakeDamage(phyDamage, magicDamage);
                                i++;
                            }
                        }
                        else
                        {
                            GameObject bulletGO = Instantiate(towerSO.bulletPrefab);
                            TestVFX bullet;
                            bulletGO.TryGetComponent<TestVFX>(out bullet);
                            if(bullet != null)
                            {
                                bullet.SetPos(firePoint.gameObject, monsters[0].gameObject);
                                monsters[0].GetComponent<Monster>().TakeDamage(phyDamage, magicDamage);
                            }     
                        }
                        isAttacked = true;
                        StartCoroutine(ResetAttack(fireRate));
                    }
                }
            }
            else
            {
                if (!isAttacked)
                {
                    if (monsters[0] == null) return;
                    RaycastHit hit;
                    Physics.Raycast(firePoint.position, (monsters[0].position - firePoint.position).normalized, out hit, attackRange, layer);
                    if (hit.transform != null && hit.transform.tag == "Monster")
                    {
                        isAttacked = true;
                        Shoot(monsters[0]);
                    }
                }
            }
        }
    }

    void Shoot(Transform m)
    {
        GameObject bulletGO = (GameObject)Instantiate(towerSO.bulletPrefab, firePoint.position, firePoint.rotation);
        TowerBullet bullet;
        bulletGO.TryGetComponent<TowerBullet>(out bullet);

        if (bullet != null)
        {
            switch (towerSO.towerType.ToString())
            {
                case "Phy":
                    bullet.Seek(m.transform, towerSO.towerType.ToString(), phyDamage, magicDamage);
                    break;
                case "Magic":
                    bullet.Seek(m.transform, towerSO.towerType.ToString(), phyDamage, magicDamage);
                    break;
                case "Fire":
                    bullet.Seek(m.transform, towerSO.towerType.ToString(), magicDamage, towerSO.burntDamage,towerSO.burntTime, towerSO.exposionRadius);
                    break;
                case "Ice":
                    bullet.Seek(m.transform, towerSO.towerType.ToString(), magicDamage, slowRatio:towerSO.slowRatio, slowTime:towerSO.slowTime);
                    break;              
            }
        }
        StartCoroutine(ResetAttack(fireRate));
    }
    IEnumerator ResetAttack(float attackDelay)
    {
        while (isAttacked)
        {
            yield return new WaitForSeconds(attackDelay);
            isAttacked = false;
        }
    }

    public void UpdateTowerSO(TowerScriptableObject towerSO)
    {
        allTowerSO.Add(towerSO);
        this.towerSO = towerSO;
        sphereCollider.radius = towerSO.attackRange;
        if (modifierVolume != null)
        {
            modifierVolume.size = new Vector3(towerSO.attackRange * 2, towerSO.attackRange * 2, towerSO.attackRange * 2);
        }
        phyDamage = 0;
        magicDamage= 0;
        for(int i=0; i<allTowerSO.Count; i++)
        {
            phyDamage += allTowerSO[i].phyDamage;
            magicDamage += allTowerSO[i].magicDamage;
        }
        attackRange = towerSO.attackRange;
        fireRate = towerSO.fireRate;
    }

    public void UpdatePearl()
    {

    }
}