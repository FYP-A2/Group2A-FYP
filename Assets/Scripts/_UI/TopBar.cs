using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUD_TopBar : MonoBehaviour
{

    public List<Transform> resourceDisplayList = new List<Transform>();

    public Player __Player;
    void Update() { ManuallyBindedUpdate_IntoTheCreatedList(resourceDisplayList); }

    public void ManuallyBindedUpdate_IntoTheCreatedList(List<Transform> list){
        for (int i = 0; i < list.Count; i++){
            //Implementation
            //Update Children's Comp's Text.
            list[i].GetComponentInChildren<TMP_Text>().text = __Player.ResourceGroup.GetAmount(i).ToString();
            list[i].GetComponentInChildren<Image>().sprite = __Player.ResourceGroup.resources[i].icon;
        }
    }
}
