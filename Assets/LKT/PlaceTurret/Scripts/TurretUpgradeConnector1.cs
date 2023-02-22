using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretUpgradeConnector1 : MonoBehaviour
{
    public bool isBase = false;

    public TurretUpgradeConnector2 connectorUp;

    public TurretUpgradeConnector2 connectorDown;

    [Serializable]
    public class Nexus
    {
        [HideInInspector]
        public TurretUpgradeConnector1 connector1;
        public TurretUpgradeConnector3 inConnector;
        public TurretUpgradeConnector3 outConnector;

        //public orbSlot;

        public bool canPlaceOre { get => connector1.CheckNexusConnectToBase(this); }
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

    bool CheckNexusConnectToBase(Nexus n)
    {
        if (isBase)
        {
            return true;
        }

        if (n.inConnector.connected)
        {
            Nexus nextN = n.inConnector.connectedConnector3.parentConnector.parentConnector.GetNexusByConnector(n.inConnector.connectedConnector3);
            if (nextN != null)
            {
                return nextN.outConnector.parentConnector.parentConnector.CheckNexusConnectToBase(nextN);
            }

        }
        return false;
    }


    public int DistanceToBase() { return DistanceToBase(0); }

    public int DistanceToBase(int n)
    {
        return isBase ? n : connectorDown.connectedConnector2.parentConnector.DistanceToBase(n + 1);
    }

    public int DistanceToTop() { return DistanceToTop(0); }

    public int DistanceToTop(int n)
    {
        return connectorUp == null ? n : connectorUp.connectedConnector2.parentConnector.DistanceToTop(n + 1);
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
}
