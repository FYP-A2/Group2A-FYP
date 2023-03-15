using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPTest : MonoBehaviour, IHP
{
   public float max = 50;
   public float now = 25;
   public UIMapHpBar umhb;

    // Start is called before the first frame update
    void Start()
   {

   }

   // Update is called once per frame
   void Update()
   {

   }

   public void GetHP(out float max, out float now)
   {
      max = this.max;
      now = this.now;
   }

   public float GetHP()
   {
      return now;
   }

   public float GetMaxHP()
   {
      return max;
   }

   //private void OnDestroy()
   //{
   //   Destroy(umhb.gameObject);
   //}
}
