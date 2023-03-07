using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 70f;
    int damage;
    Vector3 direction;
    string parentTag;
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(SelfDestory());
    }
    public void Shoot(Vector3 dir,int damage, GameObject GO)
    {
        parentTag = GO.tag;
        direction= dir;
        this.damage = damage;
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction*speed*Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other.transform.tag != "AttackArea" && other.transform.tag != parentTag)
            {
                if (other.TryGetComponent<IDamage>(out IDamage Idamage))
                {
                    Idamage.TakeDamage(damage);
                    Destroy(gameObject);
                }
                else
                    Destroy(gameObject);
            }
        }
    }

    IEnumerator SelfDestory()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            Destroy(gameObject);
        }
    }
}
