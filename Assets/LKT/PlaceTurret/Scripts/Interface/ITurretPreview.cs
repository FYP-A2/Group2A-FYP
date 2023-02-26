using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static FYP2A.VR.PlaceTurret.TurretPreview;

public interface ITurretPreview
{
    public void Initialize(GameObject previewCreator, TowerScriptableObject towerScriptableObject);

    public void SetPosition(Vector3 position);

    public void SetDisplayHint(string msg);
}
