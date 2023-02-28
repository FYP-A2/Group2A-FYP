using UnityEngine;

public class Breakable : MonoBehaviour, IDamage
{
    public int hp = 100;

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if(hp < 0) Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
