using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterDictionary {

    public string type;
    public GameObject prefab;

    public RegisteredMonsterPrefabs RMP;
    //public void SetMonsterDictionary(string type){
    //    switch(type){
    //        case "Melee":
    //            prefab = RMP.reg_Mon_Pref_1;
    //            break;
    //        case "Ranged": 
    //            prefab=RMP.reg_Mon_Pref_2; 
    //            break;
    //        default: break;
    //    }
    //}

    



    public List<float> spawnDelays_WhereEachItemMeans_In_A_Level;

    public List<int> numToSpawn_WhereEachItemMeans_In_A_Level;

    public List<int> whichSpawn;
    void Start(){
        //prefab=RMP.reg_Mon_Pref_2;
    }
}