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
    List<TurretUpgradeConnector3> _childrenConnector3 = new List<TurretUpgradeConnector3>();

    public enum Connector2Type { UP, DOWN }
    [HideInInspector]
    public Connector2Type connectorType = Connector2Type.UP;

    public bool Connected { get => _connectedConnector2.Count >= 1; }
    public TurretUpgradeConnector2 ConnectedConnector2 { get => _connectedConnector2.Count > 0 ? _connectedConnector2[0] : null; }
    public List<TurretUpgradeConnector2> _connectedConnector2 = new List<TurretUpgradeConnector2>();
    public List<TurretUpgradeConnector2> connectedConnector2_unconfirm = new List<TurretUpgradeConnector2>();

    [HideInInspector]
    public bool confirmedConnection = false;

    public void SetRelation(TurretUpgradeConnector1 parentConnector, List<TurretUpgradeConnector3> connectorChildren, Connector2Type connectorType)
    {
        this.parentConnector = parentConnector;
        this._childrenConnector3 = connectorChildren;
        this.connectorType = connectorType;

        if (_childrenConnector3 != null && _childrenConnector3.Count > 0)
            foreach(TurretUpgradeConnector3 tuc3 in _childrenConnector3)
                if (tuc3 != null)
                    tuc3.parentConnector = this;
    }

    int CheckConnectedChildren()
    {
        int i = 0;

        foreach (TurretUpgradeConnector3 child in _childrenConnector3)
            if (child.Connected)
                i++;

        return i;
    }

    public void ConfirmConnection()
    {
        StartCoroutine(KeepConfirmConnection());
    }

    IEnumerator KeepConfirmConnection()
    {
        Debug.Log("Turret:  connection start");
        int i = 0;
        while (ConnectedConnector2 == null)
        {
            yield return null;

            i++;
        }

        confirmedConnection = true;
        ConnectionCheckOnOff(false);
        ConnectedConnector2.confirmedConnection = true;
        ConnectedConnector2.ConnectionCheckOnOff(false);

        parentConnector.NexusPearlDisplay();

        Debug.Log("Turret:  connection done, time: " + i);
    }

    public void ConnectionCheckOnOff(bool on)
    {
        if (_childrenConnector3 != null && _childrenConnector3.Count > 0)
            foreach (TurretUpgradeConnector3 tuc3 in _childrenConnector3)
                if (tuc3 != null)
                    tuc3.enabled = on;

        enabled = on;
    }

    private void FixedUpdate()
    {
        _connectedConnector2 = new List<TurretUpgradeConnector2>();
    }

    private void OnTriggerStay(Collider other)
    {
        TurretUpgradeConnector2 temp;
        if (other.TryGetComponent<TurretUpgradeConnector2>(out temp) && temp.connectorType != connectorType && !_connectedConnector2.Contains(temp))
            _connectedConnector2.Add(other.GetComponent<TurretUpgradeConnector2>());
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    TurretUpgradeConnector2 temp;
    //    if (other.TryGetComponent<TurretUpgradeConnector2>(out temp) && _connectedConnector2.Contains(temp))
    //        _connectedConnector2.Remove(other.GetComponent<TurretUpgradeConnector2>());
    //}
}
