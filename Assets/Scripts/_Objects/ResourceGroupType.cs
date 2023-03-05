using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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
        public Sprite icon;
        public int amount;
    }

    public Resource FindResourceByName(string name)
    {
        return resources.Find(x => x.name == name);
    }

    public int GetAmount(string name)
    {
        Resource r = resources.Find(x => x.name == name);
        if (r!=null)
            return r.amount;
        return 0;
    }

    public int GetAmount(int index)
    {
        Resource r = resources[index];
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

    public void SetAmount(int index, int amount)
    {
        Resource r = resources[index];
        if (r != null)
            r.amount = amount;
    }

    public void AddAmount(string name, int amount)
    {
        Resource r = resources.Find(x => x.name == name);
        if (r != null)
            r.amount += amount;
    }

    public void AddAmount(int index, int amount)
    {
        Resource r = resources[index];
        if (r != null)
            r.amount += amount;
    }

    public void RemoveAmount(string name, int amount)
    {
        AddAmount(name, -amount);
    }

    public void RemoveAmount(int index, int amount)
    {
        AddAmount(index, -amount);
    }
}