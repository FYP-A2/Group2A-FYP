using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public ResourceGroupType resourceGroupType;

    int HP=5;

    public enum State{BeenCut=0,Youth=1,QuiteMature=2,Matured=3}
    public State TreeState = State.Matured;

    bool beenCutFlag=false;

    // Update is called once per frame
    void Update()
    {
        if(HP==0){
            BeingCut();
        }

        if(beenCutFlag){
            RewardPlayers();
            beenCutFlag=false;
        }
        
    }

    public void RewardPlayers(){
        resourceGroupType.Add("Wood",50);
    }

    public void BeingCut(){
        this.TreeState = State.BeenCut;
        beenCutFlag=true;
    }
}
