using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oldTower : MonoBehaviour
{
    public Transform target;
    public float attackRange = 15f;

    public float fireRate = 1f;
    float fireCountdown = 0f;

    public GameObject bulletPrefab;
    public Vector3 firePointOffset;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;

        if(fireCountdown <= 0f)
        {
            //Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Monster");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance )
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null && shortestDistance <= attackRange)
        {
            target = nearestEnemy.transform;
        }
        else
            target= null;
    }

    //void Shoot()
    //{
    //    GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, transform.position + firePointOffset, transform.rotation);
    //    oldBullet bullet = bulletGO.GetComponent<oldBullet>();

    //    if(bullet != null)
    //    {
    //        bullet.Seek(target);
    //    }
    //}
}