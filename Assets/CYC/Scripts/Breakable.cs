using UnityEngine;
using UnityEngine.AI;

public class Breakable : MonoBehaviour, IDamage,IHP
{
    public int hp = 100;
    public int maxHp;

    public void GetHP(out float max, out float now)
    {
        max = GetMaxHP();
        now = GetHP();
    }

    public float GetHP()
    {
        return hp;
    }

    public float GetMaxHP()
    {
        return maxHp;
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if(hp < 0)
        {
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            TryGetComponent<NavMeshObstacle>(out NavMeshObstacle nmo);
            if (nmo != null)
                nmo.enabled = false;
        }
    }

    public void GateReset()
    {
        TryGetComponent<NavMeshObstacle>(out NavMeshObstacle nmo);
        if (nmo != null)
        {
            hp = (int)GetMaxHP();
            GetComponent<Renderer>().enabled = true;
            GetComponent<Collider>().enabled = true;
            nmo.enabled = true;
        }
    }
    void Start()
    {
        maxHp = hp;
    }

    void Update()
    {

    }
}
