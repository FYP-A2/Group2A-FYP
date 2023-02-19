using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurrentPrefabIndex : MonoBehaviour
{
    [Serializable]
    public struct Turrent
    {
        public int tier;
        public string name;
        public string description;
        public GameObject prefab;
        public GameObject preview;
        public Resources neededResources;
    }

    [Serializable]
    public struct Resources
    {
        public int woodAmount, stoneAmount, coinAmount, fireOreAmount, iceOreAmount, toxicOreAmount, physicalOreAmount, lightningOreAmount;
    }

    [SerializeField]
    public List<Turrent> turrentsBase = new List<Turrent>();
    [SerializeField]
    public List<Turrent> turrentsUpgrade = new List<Turrent>();
}
