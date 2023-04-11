using UnityEngine;
using UnityEngine.Diagnostics;

public class _{
   //L for Log.
   public static void L(string msg){Debug.Log(msg);}
   public static void L(string symbol,string item_to_Test, string msg){Debug.Log(symbol+" "+item_to_Test+": "+msg);}
   
   public static void IMKK(string symbol,string item_to_Test){Debug.Log(symbol+" "+item_to_Test+": 今 こ↑こ↓。");}


   public static void _1(string symbol,string item_to_Test, string? msg){Debug.Log("1 "+symbol+" "+item_to_Test+": "+msg);}
   public static void _2(string symbol,string item_to_Test, string? msg, ref bool runned){if(!runned)Debug.Log("2 "+symbol+" "+item_to_Test+": "+msg);runned=true;}
   public static void _3(string symbol,string item_to_Test, string? msg){Debug.Log("3 "+symbol+" "+item_to_Test+": "+msg);}
   public static void _4(string symbol,string item_to_Test, string? msg){Debug.Log("4 "+symbol+" "+item_to_Test+": "+msg);}
   public static void _5(string symbol,string item_to_Test, string? msg){Debug.Log("5 "+symbol+" "+item_to_Test+": "+msg);}
   public static void _6(string symbol,string item_to_Test, string? msg){Debug.Log("6 "+symbol+" "+item_to_Test+": "+msg);}
   public static void _7(string symbol,string item_to_Test, string? msg){Debug.Log("7 "+symbol+" "+item_to_Test+": "+msg);}
   public static void _8(string symbol,string item_to_Test, string? msg){Debug.Log("8 "+symbol+" "+item_to_Test+": "+msg);}
   public static void _9(string symbol,string item_to_Test, string? msg){Debug.Log("9 "+symbol+" "+item_to_Test+": "+msg);}
   public static void _10(string symbol,string item_to_Test, string? msg){Debug.Log("10 "+symbol+" "+item_to_Test+": "+msg);}
   public static void _11(string symbol,string item_to_Test, string? msg){Debug.Log("11 "+symbol+" "+item_to_Test+": "+msg);}
   public static void _12(string symbol,string item_to_Test, string? msg){Debug.Log("12 "+symbol+" "+item_to_Test+": "+msg);}
}