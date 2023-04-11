using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grow : MonoBehaviour
{
    public GameObject[] plant = new GameObject[3];
    public int growState;
    public float time1to2, time2to3;
    public GameObject wood;
    // Start is called before the first frame update
    void Start()
    {
        growState = 2;//default
        SetGrow(growState);
    }

    public void SetGrow(int x) {
        growState = x;
        for (int y = 0; y < 3; y++)
        { 
            if (y==x)
                plant[y].SetActive(true);
            else
                plant[y].SetActive(false);
        }
    }

    IEnumerator GrowUp() {
        yield return new WaitForSeconds(time1to2);
        SetGrow(1);
        yield return new WaitForSeconds(time2to3);
        SetGrow(2);
    }

     public void TreeGetCut() {
         if (growState == 2)
         {
            SetGrow(0);
            // players.wood.add(10);
                    
            Debug.Log("get 10");
           // wood.GetComponent<item>().updatawood(10);
            StartCoroutine(GrowUp());//Start to re-grow
         }
     }
}
