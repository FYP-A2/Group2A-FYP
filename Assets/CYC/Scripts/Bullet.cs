using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 70f;
    int damage;
    Vector3 direction;
    GameObject parent;
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(SelfDestory());
    }
    public void Shoot(Vector3 dir,int damage, GameObject GO)
    {
        parent = GO;
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
        IDamage Idamage;
        other.TryGetComponent<IDamage>(out Idamage);
        if (Idamage != null)
        {
            Idamage.TakeDamage(damage);
            Destroy(gameObject);
        }
        else if(other.transform.tag == parent.tag)
        {

        }
        else
            Destroy(gameObject);
    }

    IEnumerator SelfDestory()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            Destroy(gameObject);
        }

    }
}
