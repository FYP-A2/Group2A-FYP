using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Mode : MonoBehaviour
{
    public enum GameMode {PAUSE=-1,DEV_MODE=0,FULL_MODE=1}
    public GameMode gameMode = GameMode.PAUSE;
    public TMP_Text TMP_Text;

    public void F11(){ gameMode=GameMode.DEV_MODE; TMP_Text.SetText("DEV MODE ON"); }
    public void F12(){ gameMode=GameMode.FULL_MODE; TMP_Text.SetText("FULL MODE ON"); }

    
}
