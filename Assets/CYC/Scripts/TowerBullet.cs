using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityStandardAssets.Effects;

public class TowerBullet : MonoBehaviour
{
    Transform target;

    public float speed = 70f;
    int phyDamage, magDamage, burntDamage;
    float burntTime, slowRatio, slowTime;
    string towerType;
    public SphereCollider sphereCollider;
    public GameObject explosion;
    List<Transform> targets;

    private void Start()
    {
        targets = sphereCollider.transform.GetComponent<AttackArea>().targets;
    }

    public void Seek(Transform target, string towerType, int phyDamage = 0, int magDamage = 0)
    {
        this.target = target;
        this.towerType = towerType;
        this.phyDamage = phyDamage;
        this.magDamage = magDamage;
    }
    public void Seek(Transform target, string towerType, int magDamage, int burntDamage, float burntTime, float exposionRadius)
    {
        this.target = target;
        this.towerType = towerType;
        this.magDamage = magDamage;
        this.burntDamage = burntDamage;
        this.burntTime = burntTime;
        sphereCollider.radius= exposionRadius;
    }

    public void Seek(Transform target, string towerType, int magDamage, float slowRatio, float slowTime)
    {
        this.target = target;
        this.towerType = towerType;
        this.magDamage = magDamage;
        this.slowRatio = slowRatio;
        this.slowTime = slowTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(transform.position-target.position);
        if (dir.magnitude <= distanceThisFrame )
        {
            HitTarget();
            return;
        }
        transform.Translate(dir.normalized*distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        //Debug.Log("Hit");
        switch (towerType) {
            case "Phy":
                target.GetComponent<Monster>().TakeDamage(phyDamage, magDamage);
                Destroy(gameObject);
                break;
            case "Magic":
                target.GetComponent<Monster>().TakeDamage(phyDamage, magDamage);
                Destroy(gameObject);
                break;
            case "Fire":
                target.GetComponent<Monster>().GetBurnt(phyDamage, magDamage, burntDamage,burntTime);
                Exposion();
                Instantiate(explosion,transform.position,Quaternion.identity);
                Destroy(gameObject);
                break;
            case "Ice":
                target.GetComponent<Monster>().GetSlow(phyDamage, magDamage,slowRatio,slowTime);
                Destroy(gameObject);
                break;           
        }
    }

    void Exposion()
    {
        foreach(Transform m in targets) {
            if (m != target)
            {
                m.GetComponent<Monster>().TakeDamage(phyDamage/4, magDamage/4);
                //Debug.Log(m.name + "hit");
            }
        }
    }

}