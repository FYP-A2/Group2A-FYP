using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretUpgradePlaceSlot : MonoBehaviour
{
    public Transform centerAlign;
    public Transform child { get { return centerAlign.childCount > 0 ? centerAlign.GetChild(0) : null; } }
    public float accura { get { return child != null ? child.localRotation.eulerAngles.y : 2 * Mathf.PI; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
