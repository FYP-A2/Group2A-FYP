using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mode : MonoBehaviour
{
    public enum GameMode {DEV_MODE=0,FULL_MODE=1}
    public GameMode gameMode = GameMode.DEV_MODE;
    public void F12(){
        if(gameMode==GameMode.DEV_MODE)gameMode=GameMode.FULL_MODE;
        else if(gameMode==GameMode.FULL_MODE)gameMode=GameMode.DEV_MODE;
    }

}
