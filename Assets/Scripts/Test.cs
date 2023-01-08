using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public int hp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getHit(int dmg)
    {
        hp -=dmg;
        if(hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
