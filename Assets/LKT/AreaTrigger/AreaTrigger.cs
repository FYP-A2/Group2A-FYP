using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AreaTrigger : MonoBehaviour
{
    public static List<AreaTrigger> areaTriggers = new List<AreaTrigger>();
    public Area_State_Script.Area_State area_State;

    public delegate void AreaTriggerEvent(Collider other);
    public AreaTriggerEvent AreaOnTriggerEnter;
    public AreaTriggerEvent AreaOnTriggerExit;

    public List<Player> playersInSide = new List<Player>();


    private void Start()
    {
        areaTriggers.Add(this);
    }

    private void OnDestroy()
    {
        areaTriggers.Remove(this);
    }


    public void OnTriggerEnter(Collider other)
    {
        AreaOnTriggerEnter(other);

        if (TryGetComponent(out Player p) && !playersInSide.Contains(p))
            playersInSide.Add(p);
    }

    public void OnTriggerExit(Collider other)
    {
        AreaOnTriggerExit(other);

        if (TryGetComponent(out Player p) && playersInSide.Contains(p))
            playersInSide.Remove(p);
    }

    public static bool CheckPlayerInArea(AreaTrigger area, Player p)
    {
        return area.playersInSide.Contains(p);
    }

    public static bool CheckPlayerInStateArea(Area_State_Script.Area_State state, Player p)
    {
        return FindAreasByState(state).Exists(x => x.playersInSide.Contains(p));
    }

    public static List<AreaTrigger> FindAreasByState(Area_State_Script.Area_State area_State)
    {
        return areaTriggers.FindAll(x => x.area_State == area_State);
    }
}
