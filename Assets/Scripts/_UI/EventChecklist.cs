using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventChecklist : MonoBehaviour
{
    public TMP_Text TMP_Text;
    public CommonTMP VR_TMP;
    public void SetText(string msg){
        if (TMP_Text != null)
            TMP_Text.SetText(msg);
        if (VR_TMP != null)
            VR_TMP.Display(msg);
    }

    public void ClearText()
    {
        if (TMP_Text != null)
            TMP_Text.SetText("");
        if (VR_TMP != null)
            VR_TMP.DisplayOff();
    }
}
