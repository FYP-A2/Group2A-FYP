using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD_TopBar : MonoBehaviour
{
    public TMP_Text label1,label2,label3;
    public TMP_Text label4,label5,label6,label7,label8;
    public TMP_Text label9,label10,label11,label12,label13;

    public Player __Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       label1.SetText(__Player.resourceGroup.woodAmount.ToString());
    }
}
