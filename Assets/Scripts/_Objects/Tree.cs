using FYP2A.VR.Melee.Target;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public MeleeTargetTree meleeTree;

    public int HP=5;
    public int NowHP { get => (int)meleeTree.nowHp; }

    [Header("Reward")]
    public string rewardResourceType = "Wood";
    public int rewardAmount = 10;

    [Header("Tree Structure")]
    public GameObject upperPart;
    public GameObject lowerPart;

    [Header("Tree Cut Down")]
    public bool hideLeaf = true;
    public Material hideLeafMaterial;
    public float autoCleanDelay = 15f;


    public enum State{BeenCut=0,Youth=1,QuiteMature=2,Matured=3}
    public State TreeState = State.Matured;

    private void Start()
    {
        meleeTree.tree = this;
        meleeTree.hp = HP;
    }

    public void BeingCut()
    {
        this.TreeState = State.BeenCut;
        RewardPlayers();
        StartCoroutine(TreeDown(autoCleanDelay));
    }

    public void RewardPlayers(){
        ResourceGroupType.Instance.Add(rewardResourceType, rewardAmount);
    }


    IEnumerator TreeDown(float autoCleanDelay)
    {
        ReleaseUpperPart();
        yield return new WaitForSeconds(autoCleanDelay);
        upperPart.SetActive(false);
    }


    void ReleaseUpperPart()
    {
        Rigidbody upperRig = upperPart.GetComponent<Rigidbody>();
        upperRig.isKinematic = false;
        upperRig.useGravity = true;
        if (hideLeaf)
            upperPart.GetComponent<Renderer>().material = hideLeafMaterial;
    }


}
