using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventChecklist : MonoBehaviour
{
    public TMP_Text TMP_Text;
    public CommonTMP VR_TMP;
    public void SetText(string msg){
        TMP_Text.SetText(msg);
        VR_TMP.Display(msg);
    }

    public void ClearText()
    {
        TMP_Text.SetText("");
        VR_TMP.DisplayOff();
    }
}
