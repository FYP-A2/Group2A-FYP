using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;

public class UICommonButton : MonoBehaviour
{
   [HideInInspector]
   public UIBook book;

   public TowerBuildSO towerBuildSO;

   [Header("")]
   public Image backgroundCompon;
   public Text textSmall;
   public Text textBig;
   public Transform previewSlot;

   [Header("")]
   public GameObject previewPrefab;
   public Vector3 previewAutoRotate;
   public Sprite background;
   public Sprite backgroundOnHover;
   public Sprite backgroundOnSelect;
   public string stringSmall;
   public string stringBig;

   [Header("")]
   public bool selecting;

   [Header("")]
   public RectTransform hoverPos;

   public delegate void Select(int n, SelectType t);
   public event Select EventSelect;
   public enum SelectType { tower, pearl }
   public SelectType selectType = SelectType.tower;

   // Start is called before the first frame update
   void Start()
   {
      //background += 
      SetPreview(previewPrefab);
      textSmall.text = stringSmall;
      textBig.text = stringBig;

      backgroundCompon.sprite = background;
      backgroundCompon.preserveAspect = true;
   }

   void Update()
   {
      AutoRotate();
   }


   void SetPreview(GameObject previewPrefab)
   {
      ClearPreviewSlot();
      Transform t = Instantiate(previewPrefab, previewSlot).transform;
      t.localPosition = Vector3.zero;
      t.localRotation = Quaternion.identity;
      t.localScale = Vector3.one;
   }

   void ClearPreviewSlot()
   {
      if (previewSlot.childCount > 0)
      {
         Destroy(previewSlot.GetChild(0).gameObject);
      }
   }

   void AutoRotate()
   {
      previewSlot.Rotate(previewAutoRotate * Time.deltaTime, Space.Self);
   }

   public void OnHoverEnter()
   {
      if (!selecting)
         backgroundCompon.sprite = backgroundOnHover;

      book.uiHover.DisplayOn( hoverPos.anchoredPosition /*+ hoverPos.GetComponentInParent<RectTransform>().anchoredPosition*/, towerBuildSO.resourceGroup);
   }

   public void OnHoverExit()
   {
      if (!selecting)
         backgroundCompon.sprite = background;

      book.uiHover.DisplayOff();
   }

   public void OnSelectEnter()
   {
      book.SelectExitAllButton();
      if (!selecting)
      {
         backgroundCompon.sprite = backgroundOnSelect;
         selecting = true;
      }
   }

   public void OnSelectExit()
   {
      if (selecting)
      {
         backgroundCompon.sprite = background;
         selecting = false;
      }
   }

   public void SelectItem(int n)
   {
      EventSelect(n, selectType);
   }

   public void Debug1()
   {
      Debug.Log("CommonButton: debug1");
   }

   public void Debug2()
   {
      Debug.Log("CommonButton: debug2");
   }
}
