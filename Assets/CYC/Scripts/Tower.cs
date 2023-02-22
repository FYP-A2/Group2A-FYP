using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Tower : MonoBehaviour
{
    [SerializeField] TowerScriptableObject towerScriptable;
    List<Transform> monsters;
    public SphereCollider sphereCollider;
    bool isAttacked;
    public LayerMask layer;
    public NavMeshModifierVolume modifierVolume;

    void Start()
    {
        //Debug.Log(gameObject.name);
        sphereCollider.radius = towerScriptable.attackRange;
        monsters = sphereCollider.transform.GetComponent<AttackArea>().targets;
        if(modifierVolume != null)
        {
            modifierVolume.size = new Vector3(towerScriptable.attackRange*2, towerScriptable.attackRange*2, towerScriptable.attackRange*2);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (monsters.Count>0)
        {
            if (towerScriptable.towerType.ToString() == "Toxic")
            {
                foreach (Transform t in monsters) {
                    IMonster m;
                    t.TryGetComponent<IMonster>(out m);
                    if (m != null)
                        m.DefenseReduction(towerScriptable.penetrationRatio, towerScriptable.penetrationTime);
                }
            }
            else if(towerScriptable.towerType.ToString() == "Ice")
            {
                if (!isAttacked)
                {
                    for (int i = 0; i < monsters.Count; i++)
                    {
                        RaycastHit hit;
                        Physics.Raycast(transform.position, (monsters[i].position - transform.position).normalized, out hit, towerScriptable.attackRange, layer);
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
                        Physics.Raycast(transform.position, (monsters[0].position - transform.position).normalized, out hit, towerScriptable.attackRange, layer);
                        if (hit.transform != null && hit.transform.tag == "Monster")
                        {
                            isAttacked = true;
                            Shoot(monsters[0]);
                        }
                    }
                }
            }
            else if (towerScriptable.towerType.ToString() == "Electro")
            {
                if (!isAttacked)
                {
                    int i = 0;
                    if(i < towerScriptable.jumpCount)
                    {
                        
                        if (monsters.Count > 1)
                        {
                            List<GameObject> bulletGO = new List<GameObject>();
                            bulletGO.Add(Instantiate(towerScriptable.bulletPrefab, transform.position,transform.rotation));
                            TestVFX bullet;
                            bulletGO[0].TryGetComponent<TestVFX>(out bullet);
                            if (bullet != null)
                            {
                                bullet.SetPos(transform.gameObject, monsters[0].gameObject);
                                monsters[0].GetComponent<Monster>().TakeDamage(towerScriptable.phyDamage, towerScriptable.magicDamage);
                            }
                            i++;
                            for (int j = 1; j < monsters.Count; j++)
                            {
                                bulletGO.Add(Instantiate(towerScriptable.bulletPrefab, monsters[j].position, monsters[j].rotation));
                                bulletGO[j].TryGetComponent<TestVFX>(out bullet);
                                bullet.SetPos(monsters[i-1].gameObject, monsters[i].gameObject);
                                monsters[j].GetComponent<Monster>().TakeDamage(towerScriptable.phyDamage, towerScriptable.magicDamage);
                                i++;
                            }
                        }
                        else
                        {
                            GameObject bulletGO = Instantiate(towerScriptable.bulletPrefab);
                            TestVFX bullet;
                            bulletGO.TryGetComponent<TestVFX>(out bullet);
                            if(bullet != null)
                            {
                                bullet.SetPos(transform.gameObject, monsters[0].gameObject);
                                monsters[0].GetComponent<Monster>().TakeDamage(towerScriptable.phyDamage, towerScriptable.magicDamage);
                            }     
                        }
                        isAttacked = true;
                        StartCoroutine(ResetAttack(towerScriptable.fireRate));
                    }
                }
            }
            else
            {
                if (!isAttacked)
                {
                    RaycastHit hit;
                    Physics.Raycast(transform.position, (monsters[0].position - transform.position).normalized, out hit, towerScriptable.attackRange, layer);
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
        GameObject bulletGO = (GameObject)Instantiate(towerScriptable.bulletPrefab, transform.position, transform.rotation);
        TowerBullet bullet;
        bulletGO.TryGetComponent<TowerBullet>(out bullet);

        if (bullet != null)
        {
            switch (towerScriptable.towerType.ToString())
            {
                case "Phy":
                    bullet.Seek(m.transform, towerScriptable.towerType.ToString(), towerScriptable.phyDamage, towerScriptable.magicDamage);
                    break;
                case "Magic":
                    bullet.Seek(m.transform, towerScriptable.towerType.ToString(), towerScriptable.phyDamage, towerScriptable.magicDamage);
                    break;
                case "Fire":
                    bullet.Seek(m.transform, towerScriptable.towerType.ToString(), towerScriptable.magicDamage, towerScriptable.burntDamage,towerScriptable.burntTime, towerScriptable.exposionRadius);
                    break;
                case "Ice":
                    bullet.Seek(m.transform, towerScriptable.towerType.ToString(), towerScriptable.magicDamage, slowRatio:towerScriptable.slowRatio, slowTime:towerScriptable.slowTime);
                    break;              
            }
        }
        StartCoroutine(ResetAttack(towerScriptable.fireRate));
    }
    IEnumerator ResetAttack(float attackDelay)
    {
        while (isAttacked)
        {
            yield return new WaitForSeconds(attackDelay);
            isAttacked = false;
        }
    }
}
