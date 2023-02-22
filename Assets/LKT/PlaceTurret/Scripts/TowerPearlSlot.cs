using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPearlSlot : MonoBehaviour
{
    public enum PearlType { Empty = 0, Speed = 1, Range = 2, AOE = 3, Phy = 4, Magic = 5 }
    public PearlType slot = PearlType.Empty;

    public List<GameObject> pearlList = new List<GameObject>();


    public void SetPearl(PearlType pt)
    {
        UndisplayAll();

        switch (pt)
        {
            case (PearlType)1: 
                slot = (PearlType)1;
                pearlList[0].SetActive(true);
                break;
            case (PearlType)2:
                slot = (PearlType)2;
                pearlList[1].SetActive(true);
                break;
            case (PearlType)3:
                slot = (PearlType)3;
                pearlList[2].SetActive(true);
                break;
            case (PearlType)4:
                slot = (PearlType)4;
                pearlList[3].SetActive(true);
                break;
            case (PearlType)5:
                slot = (PearlType)5;
                pearlList[4].SetActive(true);
                break;
        }
    }

    void UndisplayAll()
    {
        foreach (var p in pearlList)
        {
            p.SetActive(false);
        }
    }
}