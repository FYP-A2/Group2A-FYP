using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfig : MonoBehaviour{
    void Start(){ QualitySettings.vSyncCount = 1; Application.targetFrameRate = 60; }
}
