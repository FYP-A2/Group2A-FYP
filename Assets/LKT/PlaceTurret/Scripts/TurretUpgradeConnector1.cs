using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretUpgradeConnector1 : MonoBehaviour, ITurretConnector
{
    public bool isBase = false;

    public TurretUpgradeConnector2 connectorUp;

    public TurretUpgradeConnector2 connectorDown;

    TowerScriptableObject towerSO;

    [Serializable]
    public class Nexus
    {
        [HideInInspector]
        public TurretUpgradeConnector1 connector1;
        public TurretUpgradeConnector3 inConnector;
        public TurretUpgradeConnector3 outConnector;

        public TowerPearlSlot towerPearlSlot;
        
        public bool Active { get => connector1.CheckNexusConnectToBase(this); }
    }
    [SerializeField]
    public List<Nexus> nexus;


    private void Start()
    {
        List<TurretUpgradeConnector3> inConnectorList = new List<TurretUpgradeConnector3>();
        List<TurretUpgradeConnector3> outConnectorList = new List<TurretUpgradeConnector3>();

        foreach (Nexus n in nexus) {
            n.connector1 = this;
            inConnectorList.Add(n.inConnector);
            outConnectorList.Add(n.outConnector);
        }

        if (connectorDown != null)
        {
            connectorDown.SetRelation(this, inConnectorList, TurretUpgradeConnector2.Connector2Type.DOWN);
        }

        if (connectorUp !=null)
        {
            connectorUp.SetRelation(this, outConnectorList, TurretUpgradeConnector2.Connector2Type.UP);
        }

        enabled = false;
    }

    public void NexusPearlDisplay()
    {
        foreach (Nexus n in nexus)
            if (n.towerPearlSlot != null)
            {
                if (n.Active)
                    n.towerPearlSlot.gameObject.SetActive(true);
                else
                    n.towerPearlSlot.gameObject.SetActive(false);
            }
    }

    bool CheckNexusConnectToBase(Nexus n)
    {
        if (isBase)
        {
            return true;
        }

        if (n.inConnector.Connected)
        {
            Nexus nextN = n.inConnector.ConnectedConnector3.parentConnector.parentConnector.GetNexusByConnector(n.inConnector.ConnectedConnector3);
            if (nextN != null)
            {
                return nextN.outConnector.parentConnector.parentConnector.CheckNexusConnectToBase(nextN);
            }

        }
        return false;
    }




    public int DistanceToBase(int n)
    {
        return isBase ? n : connectorDown.ConnectedConnector2.parentConnector.DistanceToBase(n + 1);
    }

    

    public int DistanceToTop(int n)
    {
        return (connectorUp == null)||(!connectorUp.Connected) ? n : connectorUp.ConnectedConnector2.parentConnector.DistanceToTop(n + 1);
    }

    public void ConnectionCheckOnOff(bool on)
    {
        connectorUp.ConnectionCheckOnOff(on);
        connectorDown.ConnectionCheckOnOff(on);
    }

    public TurretUpgradeConnector3 GetInConnectorByOutConnector(TurretUpgradeConnector3 OutConnector)
    {
        if (OutConnector == null)
            return null;

        foreach (Nexus n in nexus)
            if (n.outConnector == OutConnector)
                return n.inConnector;

        return null;
    }

    public Nexus GetNexusByConnector(TurretUpgradeConnector3 connector)
    {
        if (connector == null)
            return null;

        foreach (Nexus n in nexus)
            if (n.outConnector == connector || n.inConnector == connector)
                return n;

        return null;
    }

    

    #region ITurretConnector;

    public void SetTowerSO(TowerScriptableObject tso)
    {
        towerSO = tso;
    }

    public TowerScriptableObject GetTowerSO()
    {
        return towerSO;
    }

    public TurretUpgradeConnector1 GetLowerConnector()
    {
        return connectorDown.ConnectedConnector2.parentConnector;
    }

    public TurretUpgradeConnector1 GetUpperConnector()
    {
        return connectorUp.ConnectedConnector2.parentConnector;
    }

    public TurretUpgradeConnector1 GetBaseConnector()
    {
        if (isBase)
            return this;
        return GetLowerConnector().GetBaseConnector();
    }

    public TurretUpgradeConnector1 GetTopConnector()
    {
        if (connectorUp == null)
            return this;
        if (!connectorUp.Connected)
            return this;
        return GetUpperConnector().GetTopConnector();
    }

    public List<TurretUpgradeConnector1> GetAllConnector()
    {
        List<TurretUpgradeConnector1> result = new List<TurretUpgradeConnector1>();
        GetBaseConnector().GetAllConnector(result,out result);

        return result;
    }

    void GetAllConnector(List<TurretUpgradeConnector1> tuclIn, out List<TurretUpgradeConnector1> tuclOut)
    {
        List<TurretUpgradeConnector1> temp = new List<TurretUpgradeConnector1>(tuclIn);
        temp.Add(this);
        if (GetDistanceToTop() != 0)
            GetUpperConnector().GetAllConnector(temp, out temp);
        tuclOut = temp;
    }

    public int GetDistanceToTop() 
    {
        return DistanceToTop(0); 
    }

    public int GetDistanceToBase() 
    { 
        return DistanceToBase(0); 
    }

    public List<TowerPearlSlot.PearlType> GetActivatedPearl()
    {
        List<TowerPearlSlot.PearlType> result = new List<TowerPearlSlot.PearlType>();
    
        foreach (Nexus n in nexus)
            if (n.Active)
                result.Add(n.towerPearlSlot.slot);
        
        return result;
    }

    public List<TowerPearlSlot.PearlType> GetAllActivatedPearl()
    {
        List<TowerPearlSlot.PearlType> result = new List<TowerPearlSlot.PearlType>();
        GetBaseConnector().GetAllActivatedPearl(result, out result);

        return result;
    }
    void GetAllActivatedPearl(List<TowerPearlSlot.PearlType> _in,out List<TowerPearlSlot.PearlType> _out)
    {
        List<TowerPearlSlot.PearlType> temp = new List<TowerPearlSlot.PearlType>(_in);
        temp.AddRange(GetActivatedPearl());
        if (GetDistanceToTop() != 0)
            GetUpperConnector().GetAllActivatedPearl(temp, out temp);
        _out = temp;
    }


    #endregion


}
