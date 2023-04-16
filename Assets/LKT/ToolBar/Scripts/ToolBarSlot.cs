using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ToolBarSlot : MonoBehaviour
{
    public ToolBar toolBar;

    Vector3 orignalPosition;
    Vector3 orignalScale;
    Quaternion orignalRotation;
    Transform t;

    XRGrabInteractable xrgi;

    public void StoreTransform()
    {
        t = transform.GetChild(0);
        orignalPosition = t.localPosition;
        orignalScale = t.localScale;
        orignalRotation = t.localRotation;
    }

    private void Update()
    {
        if (xrgi != null && xrgi.isSelected)
            toolBar.Deactivate();
            
    }

    public void RestoreTransform()
    {
        if (t.TryGetComponent(out xrgi))
        {
            if (!xrgi.isSelected)
            {
                if (t.parent != transform)
                    t.SetParent(transform);
                t.localPosition = orignalPosition;
                t.localScale = orignalScale;
                t.localRotation = orignalRotation;
            }
        }
        else
        {
            if (t.parent != transform)
                t.SetParent(transform);
            t.localPosition = orignalPosition;
            t.localScale = orignalScale;
            t.localRotation = orignalRotation;
        }
    }
}
