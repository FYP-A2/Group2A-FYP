using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlaceTurret
{
    public void SetTurrentPrefabIndex(TurretPrefabIndex tpi);
    public void SetPreviewTurret(int towerID);
    //towerID is the index of an TowerBuildScriptableObject in TurretPrefabIndex.
    //In order to get the towerID and use this method,
    //you need to use SetTurrentPrefabIndex(TurretPrefabIndex tpi) to set TurretPrefabIndex firstly.


}
