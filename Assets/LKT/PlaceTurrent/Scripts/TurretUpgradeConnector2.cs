using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static TurretUpgradeConnector2;

public class TurretUpgradeConnector2 : MonoBehaviour
{
    [HideInInspector]
    public TurretUpgradeConnector1 parentConnector;
    [HideInInspector]
    public List<TurretUpgradeConnector3> childrenConnector = new List<TurretUpgradeConnector3>();

    public enum Connector2Type { UP, DOWN }
    [HideInInspector]
    public Connector2Type connectorType = Connector2Type.UP;

    public bool connected { get => _connectedConnector2.Count == 1; }
    public TurretUpgradeConnector2 connectedConnector2 { get => _connectedConnector2.Count > 0 ? _connectedConnector2[0] : null; }
    public List<TurretUpgradeConnector2> _connectedConnector2 = new List<TurretUpgradeConnector2>();

    [HideInInspector]
    public bool confirmedConnection = false;

    public void SetRelation(TurretUpgradeConnector1 parentConnector, List<TurretUpgradeConnector3> connectorChildren, Connector2Type connectorType)
    {
        this.parentConnector = parentConnector;
        this.childrenConnector = connectorChildren;
        this.connectorType = connectorType;

        if (childrenConnector != null && childrenConnector.Count > 0)
            foreach(TurretUpgradeConnector3 tuc3 in childrenConnector)
                if (tuc3 != null)
                    tuc3.parentConnector = this;
    }

    int CheckConnectedChildren()
    {
        int i = 0;

        foreach (TurretUpgradeConnector3 child in childrenConnector)
            if (child.connected)
                i++;

        return i;
    }

    public void ConfirmConnection()
    {
        confirmedConnection = true;
        ConnectionCheckOnOff(false);
        connectedConnector2.confirmedConnection = true;
        connectedConnector2.ConnectionCheckOnOff(false);
    }

    public void ConnectionCheckOnOff(bool on)
    {
        if (childrenConnector != null && childrenConnector.Count > 0)
            foreach (TurretUpgradeConnector3 tuc3 in childrenConnector)
                tuc3.enabled = on;

        enabled = on;
    }

    private void OnTriggerEnter(Collider other)
    {
        TurretUpgradeConnector2 temp;
        if (other.TryGetComponent<TurretUpgradeConnector2>(out temp) && temp.connectorType != connectorType)
            _connectedConnector2.Add(other.GetComponent<TurretUpgradeConnector2>());
    }

    private void OnTriggerExit(Collider other)
    {
        TurretUpgradeConnector2 temp;
        if (other.TryGetComponent<TurretUpgradeConnector2>(out temp) && _connectedConnector2.Contains(temp))
            _connectedConnector2.Remove(other.GetComponent<TurretUpgradeConnector2>());
    }
}
