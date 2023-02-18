using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Tower : MonoBehaviour
{
    [SerializeField] TowerScriptableObject towerScriptable;
    List<Transform> monsters;
    public SphereCollider sphereCollider;
    bool isAttacked;
    public LayerMask layer;

    void Start()
    {
        sphereCollider.radius = towerScriptable.attackRange;
        monsters = sphereCollider.transform.GetComponent<AttackArea>().targets;
    }

    // Update is called once per frame
    void Update()
    {
        if (monsters.Count>0)
        {
            if (!isAttacked)
            {
                RaycastHit hit;
                Physics.Raycast(transform.position, (monsters[0].position - transform.position).normalized, out hit, towerScriptable.attackRange,layer);
                if (hit.transform != null && hit.transform.tag == "Monster")
                {
                    isAttacked = true;
                    Shoot(monsters[0]);
                }
            }
        }
    }

    void Shoot(Transform m)
    {
        GameObject bulletGO = (GameObject)Instantiate(towerScriptable.bulletPrefab, transform.position, transform.rotation);
        TowerBullet bullet = bulletGO.GetComponent<TowerBullet>();

        if (bullet != null)
        {
            switch (towerScriptable.Type)
            {
                case "Phy":
                    bullet.Seek(m.transform, towerScriptable.Type, towerScriptable.phyDamage);
                    break;
                case "Fire":
                    bullet.Seek(m.transform, towerScriptable.Type, towerScriptable.magicDamage, burntDamage:towerScriptable.burntDamage,burntTime:towerScriptable.burntTime);
                    break;
                case "Ice":
                    bullet.Seek(m.transform, towerScriptable.Type, towerScriptable.magicDamage, slowRatio:towerScriptable.slowRatio, slowTime:towerScriptable.slowTime);
                    break;
                case "Electro":
                    bullet.Seek(m.transform, towerScriptable.Type, towerScriptable.magicDamage, jumpCount:towerScriptable.jumpCount);
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
