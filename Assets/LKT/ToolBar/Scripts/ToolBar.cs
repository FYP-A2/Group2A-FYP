using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ToolBar : MonoBehaviour
{
   public List<ToolBarSlot> slots = new List<ToolBarSlot>();
   Vector3 orignalScale;

   // Start is called before the first frame update
   void Start()
   {
      foreach (var slot in slots)
         slot.StoreTransform();

      orignalScale = transform.localScale;

      gameObject.SetActive(false);
   }

   public void Activate()
   {
      if (!gameObject.activeSelf)
      {
         foreach (var slot in slots)
            slot.RestoreTransform();
         gameObject.SetActive(true);
         StartCoroutine(ActivateAnimation());
      }
   }

   IEnumerator ActivateAnimation()
   {
      transform.localScale = Vector3.one * 0.000001f;
      transform.eulerAngles = new Vector3(0,45,0);
      float t = 0;
      while (t<0.3f)
      {
         transform.localScale = Vector3.Lerp(transform.localScale, orignalScale, t);
         transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0,0,0), t);
         t += Time.deltaTime;
         yield return null;
      }
      transform.localScale = orignalScale;
   }

   public void Deactivate()
   {
      if (gameObject.activeSelf)
      {
         StartCoroutine(DeactivateAnimation());
      }
   }

   IEnumerator DeactivateAnimation()
   {
      transform.localScale = orignalScale;
      Vector3 finalScale = Vector3.one * 0.000001f;
      transform.eulerAngles = new Vector3(0, 0, 0);
      float t = 0;
      while (t < 0.3f)
      {
         transform.localScale = Vector3.Lerp(transform.localScale, finalScale, t);
         transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, 45, 0), t);
         t += Time.deltaTime;
         yield return null;
      }

      gameObject.SetActive(false);
   }
}
