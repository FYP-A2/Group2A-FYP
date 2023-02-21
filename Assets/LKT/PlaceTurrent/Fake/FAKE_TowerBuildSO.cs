using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tower Build SO", menuName = "ScriptableObjects.TowerBuildSO")]
public class TowerBuildSO : ScriptableObject
{
    public TowerScriptableObject Tower;
    public GameObject towerPreview;
    public Resources neededResources;

    [Serializable]
    public struct Resources {
        public int wood;
        public int stone;
        public int Phy;
        public int Fire;
        public int Ice;
        public int Toxic;
        public int Electro;
    }
}