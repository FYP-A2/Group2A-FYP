using FYP2A.VR.PlaceTurrent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPlacePC : PlaceTurret
{
    [Header("PC control")]
    Transform rayDirection;

    protected override Ray GetRay()
    {
        return new Ray(transform.position, transform.forward);
    }
}
