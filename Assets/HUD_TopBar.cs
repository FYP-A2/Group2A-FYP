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
       //label1.SetText(__Player.resourceGroup.woodAmount.ToString());
       //label2.SetText(__Player.resourceGroup.stoneAmount.ToString());
       //label3.SetText(__Player.resourceGroup.coinAmount.ToString());
       //
       //label4.SetText(__Player.resourceGroup.iceOreAmount.ToString());
       //label5.SetText(__Player.resourceGroup.fireOreAmount.ToString());
       //label6.SetText(__Player.resourceGroup.toxicOreAmount.ToString());
       //label7.SetText(__Player.resourceGroup.physicalOreAmount.ToString());
       //label8.SetText(__Player.resourceGroup.electroOreAmount.ToString());
       //
       //label9.SetText(__Player.resourceGroup.icePearlAmount.ToString());
       //label10.SetText(__Player.resourceGroup.firePearlAmount.ToString());
       //label11.SetText(__Player.resourceGroup.toxicPearlAmount.ToString());
       //label12.SetText(__Player.resourceGroup.physicalPearlAmount.ToString());
       //label13.SetText(__Player.resourceGroup.electroPearlAmount.ToString());


        label1.SetText(__Player.resourceGroup.GetAmount("woodAmount").ToString());
        label2.SetText(__Player.resourceGroup.GetAmount("stoneAmount").ToString());
        label3.SetText(__Player.resourceGroup.GetAmount("coinAmount").ToString());

        label4.SetText(__Player.resourceGroup.GetAmount("iceOreAmount").ToString());
        label5.SetText(__Player.resourceGroup.GetAmount("fireOreAmount").ToString());
        label6.SetText(__Player.resourceGroup.GetAmount("toxicOreAmount").ToString());
        label7.SetText(__Player.resourceGroup.GetAmount("physicalOreAmount").ToString());
        label8.SetText(__Player.resourceGroup.GetAmount("electroOreAmount").ToString());

        label9.SetText(__Player.resourceGroup.GetAmount("icePearlAmount").ToString());
        label10.SetText(__Player.resourceGroup.GetAmount("firePearlAmount").ToString());
        label11.SetText(__Player.resourceGroup.GetAmount("toxicPearlAmount").ToString());
        label12.SetText(__Player.resourceGroup.GetAmount("physicalPearlAmount").ToString());
        label13.SetText(__Player.resourceGroup.GetAmount("electroPearlAmount").ToString());
    }
}
