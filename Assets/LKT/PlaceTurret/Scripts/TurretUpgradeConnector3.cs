using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretUpgradeConnector3 : MonoBehaviour
{
    [HideInInspector]
    public TurretUpgradeConnector2 parentConnector;
    public bool Connected { get => _connectedConnector3.Count >= 1; }
    public TurretUpgradeConnector3 ConnectedConnector3 { get => _connectedConnector3.Count > 0 ? _connectedConnector3[_connectedConnector3.Count - 1] : null; }
    public List<TurretUpgradeConnector3> _connectedConnector3 = new List<TurretUpgradeConnector3>();
    //public OreSlot oreSlot;

    private void FixedUpdate()
    {
        _connectedConnector3 = new List<TurretUpgradeConnector3>();
    }

    private void OnTriggerStay(Collider other)
    {
        TurretUpgradeConnector3 temp;
        if (other.TryGetComponent<TurretUpgradeConnector3>(out temp) && temp.parentConnector.connectorType != parentConnector.connectorType && !_connectedConnector3.Contains(temp))
            _connectedConnector3.Add(other.GetComponent<TurretUpgradeConnector3>());
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    TurretUpgradeConnector3 temp;
    //    if (other.TryGetComponent<TurretUpgradeConnector3>(out temp) && _connectedConnector3.Contains(temp))
    //        _connectedConnector3.Remove(other.GetComponent<TurretUpgradeConnector3>());
    //}
}
