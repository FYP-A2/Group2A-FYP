using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Transform target;

    public float speed = 70f;
    public int damage = 20;
    public void Seek(Transform target)
    {
        this.target = target;
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

        if(dir.magnitude <= distanceThisFrame )
        {
            HitTarget();
            return;
        }
        transform.Translate(dir.normalized*distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        //Debug.Log("Hit");
        target.GetComponent<Monster>().TakeDamage(damage);
        Destroy(gameObject);
    }
}
