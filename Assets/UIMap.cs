using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMap : MonoBehaviour
{
   public static float scale;
   public float scaleControl = 30f;
   public float scaleUITagFactor = 8.5f;
   public UIBook book;
   public Camera cam;

   void Update()
   {
      //if is owner
      cam.orthographicSize = scaleControl;
      scale = scaleControl * (scaleUITagFactor/10000);
   }
}
