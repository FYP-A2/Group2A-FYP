using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit.UI;

public class UIMap : MonoBehaviour
{
   public static float scale;
   public float scaleControl = 30f;
   public float scaleUITagFactor = 8.5f;
   public UIBook book;
   public Camera cam;
   public Vector2 DragPoint;

   void Update()
   {
      //if is owner
      cam.orthographicSize = scaleControl;
      scale = scaleControl * (scaleUITagFactor/10000);
   }

   public void OnDrag(BaseEventData data)
   {
      PointerEventData pointer = (PointerEventData)data;
      RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(), pointer.position, book.playerCam, out DragPoint);
      Debug.Log(DragPoint.ToString());
      // TrackedDeviceEventData vrEventData = (TrackedDeviceEventData)data;
   }
}
