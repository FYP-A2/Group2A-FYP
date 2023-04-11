using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static TurretUpgradeConnector1;

public interface ITurretConnector
{
    public void SetTowerSO(TowerScriptableObject tso);

    public TowerScriptableObject GetTowerSO();

    public TurretUpgradeConnector1 GetLowerConnector();

    public TurretUpgradeConnector1 GetUpperConnector();

    public TurretUpgradeConnector1 GetBaseConnector();

    public TurretUpgradeConnector1 GetTopConnector();

    public List<TurretUpgradeConnector1> GetAllConnector();

    public int GetDistanceToTop();

    public int GetDistanceToBase();

    public List<TowerPearlSlot.PearlType> GetActivatedPearl();

    public List<TowerPearlSlot.PearlType> GetAllActivatedPearl();
}
