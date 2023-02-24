using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabbingPC : MonoBehaviour
{
    [Header("Grabbing Now")]
    public Transform grabbingNow;
    public Transform grabbingNowOriginParent;
    public bool isGrabbing = false;

    [Header("Input")]
    public Transform raySource;
    public InputActionProperty primary;
    public InputActionProperty rotateLocX;
    public InputActionProperty rotateLocY;
    public InputActionProperty rotateLocZ;
    public InputActionProperty positLocZ;

    [Header("Hold Slot")]
    public Transform HoldSlot;

    private void OnEnable()
    {
        primary.action.Enable();
        rotateLocX.action.Enable();
        rotateLocY.action.Enable();
        rotateLocZ.action.Enable();
        positLocZ.action.Enable();
    }

    private void Start()
    {

        primary.action.performed += Primary_performed;
        rotateLocX.action.performed += RotateLocX_performed;
        rotateLocY.action.performed += RotateLocY_performed;
        rotateLocZ.action.performed += RotateLocZ_performed;
        positLocZ.action.performed += PositLocZ_performed;
    }



    private void Primary_performed(InputAction.CallbackContext obj)
    {
        if (isGrabbing)
        {
            RemoveItem();
            isGrabbing = false;
        }
        else
        {
            RaycastHit hit;
            XRGrabInteractable interactable;
            Physics.Raycast(new Ray(raySource.position, raySource.forward),out hit);
            Debug.Log("GrabbingPC :   hit name :" + hit.transform.name.ToString());
            if(hit.transform != null && hit.transform.TryGetComponent<XRGrabInteractable>(out interactable))
            {
                SetItem(hit.transform);
                isGrabbing = true;
            }
        }
    }

    private void RotateLocX_performed(InputAction.CallbackContext obj)
    {
        throw new System.NotImplementedException();
    }

    private void RotateLocY_performed(InputAction.CallbackContext obj)
    {
        throw new System.NotImplementedException();
    }

    private void RotateLocZ_performed(InputAction.CallbackContext obj)
    {
        throw new System.NotImplementedException();
    }

    private void PositLocZ_performed(InputAction.CallbackContext obj)
    {
        HoldSlot.localPosition += new Vector3(0,0,positLocZ.action.ReadValue<float>())/100;
    }


    void SetItem(Transform item)
    {
        grabbingNow = item;
        grabbingNowOriginParent = item.parent;
        item.SetParent(HoldSlot);
        item.localPosition = Vector3.zero;
        item.localRotation = Quaternion.identity;
    }

    void RemoveItem()
    {
        grabbingNow.SetParent(grabbingNowOriginParent);
        grabbingNow = null;
        grabbingNowOriginParent = null;
    }

    
}
