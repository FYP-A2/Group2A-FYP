using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPearlSlot : MonoBehaviour
{
    public enum PearlType { Empty = 0, Phy = 1, Fire = 2, Ice = 3, Toxic = 4, Electro = 5 }
    public PearlType slot = PearlType.Empty;

    public List<GameObject> pearlList = new List<GameObject>();


    public void SetPearl(PearlType pt)
    {
        UndisplayAll();

        switch (pt)
        {
            case PearlType.Phy: 
                slot = PearlType.Phy;
                pearlList[0].SetActive(true);
                break;
            case PearlType.Fire:
                slot = PearlType.Fire;
                pearlList[1].SetActive(true);
                break;
            case PearlType.Ice:
                slot = PearlType.Ice;
                pearlList[2].SetActive(true);
                break;
            case PearlType.Toxic:
                slot = PearlType.Toxic;
                pearlList[3].SetActive(true);
                break;
            case PearlType.Electro:
                slot = PearlType.Electro;
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