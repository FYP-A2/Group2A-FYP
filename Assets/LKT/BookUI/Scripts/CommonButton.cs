using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;

public class CommonButton : MonoBehaviour
{
    public Image background;
    public Text textSmall;
    public Text textBig;
    public Transform previewSlot;

    [Header("")]
    public GameObject previewPrefab;
    public Vector3 previewAutoRotate;


    // Start is called before the first frame update
    void Start()
    {
        //background += 
        SetPreview(previewPrefab);
    }

    void Update()
    {
        AutoRotate();
    }


    void SetPreview(GameObject previewPrefab)
    {
        ClearPreviewSlot();
        Transform t = Instantiate(previewPrefab,previewSlot).transform;
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
        previewSlot.Rotate(previewAutoRotate*Time.deltaTime,Space.Self);
    }
}
