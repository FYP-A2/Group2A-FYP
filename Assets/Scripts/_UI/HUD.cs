using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public GameObject HUD1,HUD2,HUD3,HUD4;

    public bool toggleF1=false;
    public bool toggleF2=false;
    public bool toggleF3=false;
    public bool toggleF4=false;

    // Start is called before the first frame update
    public void F1(){if(!toggleF1){toggleF1=true;HUD1.SetActive(true);} else{toggleF1=false;HUD1.SetActive(false);}}
    public void F2(){if(!toggleF2){toggleF2=true;HUD2.SetActive(true);} else{toggleF2=false;HUD2.SetActive(false);}}
    public void F3(){if(!toggleF3){toggleF3=true;HUD3.SetActive(true);} else{toggleF3=false;HUD3.SetActive(false);}}
    public void F4(){if(!toggleF4){toggleF4=true;HUD4.SetActive(true);} else{toggleF4=false;HUD4.SetActive(false);}}
}
