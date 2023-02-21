using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public TowerScriptableObject test;
    [SerializeField] GameObject currentTower;
    [SerializeField] TowerScriptableObject currentTowerScriptable;
    TowerScriptableObject prev, current;

    public void BuildTower(TowerScriptableObject towerScriptable)
    {
        currentTowerScriptable = towerScriptable;
        if (currentTower != null)
        {
            Destroy(currentTower);
        }

        currentTower = Instantiate(towerScriptable.towerPrefab, transform);
        currentTower.transform.localPosition = Vector3.zero;
        currentTower.transform.localRotation = Quaternion.identity;
    }


}