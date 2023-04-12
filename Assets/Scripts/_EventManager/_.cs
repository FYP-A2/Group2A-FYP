using UnityEngine;
using UnityEngine.Diagnostics;

public class _{
   //L for Log.
   public static void L(string msg){Debug.Log(msg);}
   public static void L(string symbol,string item_to_Test, string msg){Debug.Log(symbol+" "+item_to_Test+": "+msg);}

}