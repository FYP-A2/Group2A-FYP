using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurrentPrefabIndex : MonoBehaviour
{
    [Serializable]
    public struct Turrent
    {
        public string name;
        public string description;
        public GameObject prefab;
        public GameObject preview;
        public Resources neededResources;
    }

    [Serializable]
    public struct Resources
    {
        public int wood;
        public int ore;
    }

    [SerializeField]
    public List<Turrent> turrents = new List<Turrent>();
}
