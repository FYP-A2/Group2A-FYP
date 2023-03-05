using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResourceGroupType
{
    //Fire Ice Toxic Normal Lightning
   //[SerializeField] public int woodAmount,stoneAmount,coinAmount;
   //[SerializeField] public int fireOreAmount,iceOreAmount,toxicOreAmount,physicalOreAmount,electroOreAmount;
   //[SerializeField] public int firePearlAmount,icePearlAmount,toxicPearlAmount,physicalPearlAmount,electroPearlAmount;

    [SerializeField]
    public List<Resource> resources;

    public ResourceGroupType(List<Resource> r)
    {
        if (r != null)
            resources = r;
        else
            resources = new List<Resource>();
    }

    [Serializable]
    public class Resource
    {
        public string name;
        public int amount;
    }

    public int GetAmount(string name)
    {
        Resource r = resources.Find(x => x.name == name);
        if (r!=null)
            return r.amount;
        return 0;
    }

    public int GetAmount(int n)
    {
        Resource r = resources[n];
        if (r != null)
            return r.amount;
        return 0;
    }

    public void SetAmount(string name, int amount)
    {
        Resource r = resources.Find(x => x.name == name);
        if (r != null)
            r.amount = amount;
    }

    public void SetAmount(int n, int amount)
    {
        Resource r = resources[n];
        if (r != null)
            r.amount = amount;
    }
}