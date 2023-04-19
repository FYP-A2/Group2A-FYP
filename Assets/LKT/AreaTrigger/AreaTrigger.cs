using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AreaTrigger : MonoBehaviour
{
    public static List<AreaTrigger> areaTriggers = new List<AreaTrigger>();
    public string area_ID = "";
    public string area_Name = "";
    public Area_State_Script.Area_State area_State;

    public delegate void AreaTriggerEvent(Collider other);
    public AreaTriggerEvent AreaOnTriggerEnter;
    public AreaTriggerEvent AreaOnTriggerExit;

    public List<Player> playersInSide = new List<Player>();


    private void Start()
    {
        areaTriggers.Add(this);
        AreaOnTriggerEnter += A;
        AreaOnTriggerExit += A;

        GetComponent<Collider>().isTrigger = true;
    }
    void A(Collider other)
    {

    }

    private void OnDestroy()
    {
        areaTriggers.Remove(this);
    }




    public void OnTriggerEnter(Collider other)
    {
        AreaOnTriggerEnter(other);

        if (other.TryGetComponent(out Player p) && !playersInSide.Contains(p))
            playersInSide.Add(p);
    }

    public void OnTriggerExit(Collider other)
    {
        AreaOnTriggerExit(other);

        if (other.TryGetComponent(out Player p) && playersInSide.Contains(p))
            playersInSide.Remove(p);
    }





    public static bool CheckPlayerInArea(AreaTrigger area, Player p)  //single area
    {
        return area.playersInSide.Contains(p);
    }

    public static bool CheckPlayerInArea(List<AreaTrigger> areas, Player p) //list of area
    {
        return areas.Exists(x => x.playersInSide.Contains(p));
    }

    public static bool CheckPlayerInAreaByName(string name, Player p) //name
    {
        return FindAreasByName(name).Exists(x => x.playersInSide.Contains(p));
    }
    
    public static bool CheckPlayerInAreaByID(string id, Player p) //id
    {
        return FindAreasByID(id).Exists(x => x.playersInSide.Contains(p));
    }

    public static bool CheckPlayerInStateArea(Area_State_Script.Area_State state, Player p) //state
    {
        return FindAreasByState(state).Exists(x => x.playersInSide.Contains(p));
    }




    public static List<AreaTrigger> FindAreasByState(Area_State_Script.Area_State area_State)
    {
        return areaTriggers.FindAll(x => x.area_State == area_State);
    }

    public static List<AreaTrigger> FindAreasByName(string name)
    {
        return areaTriggers.FindAll(x => x.area_Name == name);
    }

    public static List<AreaTrigger> FindAreasByID(string id)
    {
        return areaTriggers.FindAll(x => x.area_ID == id);
    }




    public static List<AreaTrigger> FindAreas(Predicate<AreaTrigger> match)
    {
        if (match == null)
            return null;

        return areaTriggers.FindAll(match);
    }
}
