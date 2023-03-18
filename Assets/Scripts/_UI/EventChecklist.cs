using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventChecklist : MonoBehaviour
{
    public TMP_Text TMP_Text;
    public void SetText(string msg){
        TMP_Text.SetText(msg);
    }
}
