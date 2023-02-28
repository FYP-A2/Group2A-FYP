using FYP2A.VR.PlaceTurret;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace FYP2A.VR.PlaceTurret
{
    public class TurretPlacePC : PlaceTurret
    {
        [Header("PC control")]
        public Transform rayDirection;

        new void Start()
        {
            base.Start();
        }

        protected override Ray GetRay()
        {
            //Debug.Log("in pc: " + rayDirectionposition);
            Debug.DrawRay(rayDirection.position, rayDirection.forward, Color.yellow, 0.3f);
            return new Ray(rayDirection.position, rayDirection.forward);
        }
    }
}
