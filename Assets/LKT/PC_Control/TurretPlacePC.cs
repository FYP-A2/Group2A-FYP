using FYP2A.VR.PlaceTurret;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FYP2A.VR.PlaceTurret
{
    public class TurretPlacePC : PlaceTurret
    {
        [Header("PC control")]
        public InputActionProperty mousePos;
        public Camera cam;

        new void OnEnable()
        {
            base.OnEnable();
            mousePos.action.Enable();
        }

        new void OnDisable()
        {
            base.OnDisable();
            mousePos.action.Disable();
        }

        new void Start()
        {
            base.Start();
        }

        protected override Ray GetRay()
        {
            return cam.ScreenPointToRay(mousePos.action.ReadValue<Vector2>());
        }
    }
}
