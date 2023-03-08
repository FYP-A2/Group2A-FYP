using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript1 : MonoBehaviour
{
    public UIMapHpBar m;
    private void OnEnable()
    {
        m.AddAmount(4);
    }
}
