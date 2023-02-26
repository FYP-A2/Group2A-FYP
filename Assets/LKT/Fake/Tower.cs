using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    // fake tower

    public void UpdateTowerSO(TowerScriptableObject tso) {
        Debug.Log("tower receive tso , tso name: " + tso.name);
    }
}
