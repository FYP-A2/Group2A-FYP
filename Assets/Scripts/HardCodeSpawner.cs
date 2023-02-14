using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardCodeSpawner : MonoBehaviour
{
    public Player p = new Player();
    public GameObject grandchild_UsedModel;
    
    void Start()
    {
        p.usedModel=grandchild_UsedModel;
        p.Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
