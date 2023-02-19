using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBullet : MonoBehaviour
{
    Transform target;

    public float speed = 70f;
    int phyDamage, magDamage, burntDamage, jumpCount;
    float burntTime, slowRatio, slowTime;
    string towerType;
    public void Seek(Transform target, string towerType, int phyDamage = 0, int magDamage = 0, int burntDamage = 0, float burntTime = 0, float slowRatio = 0, float slowTime = 0, int jumpCount = 0)
    {
        this.target = target;
        this.towerType = towerType;
        this.phyDamage = phyDamage;
        this.magDamage = magDamage;
        this.burntDamage = burntDamage;
        this.burntTime= burntTime;
        this.slowRatio = slowRatio;
        this.slowTime = slowTime;
        this.jumpCount = jumpCount;
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
        //transform.rotation = Quaternion.LookRotation(Vector3.forward);
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
            case "Fire":
                target.GetComponent<Monster>().GetBurnt(phyDamage, magDamage, burntDamage,burntTime);
                Destroy(gameObject);
                break;
            case "Ice":
                target.GetComponent<Monster>().GetSlow(phyDamage, magDamage,slowRatio,slowTime);
                Destroy(gameObject);
                break;
            case "Electro":
                target.GetComponent<Monster>().TakeDamage(phyDamage, magDamage);
                Destroy(gameObject);
                break;
        }
    }
}
