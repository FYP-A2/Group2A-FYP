using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateButton : MonoBehaviour
{
    public GameObject buttonIN;
    public GameObject buttonOUT;
    public bool isOpen = false;
    public Vector3 gateOpenDistance = new Vector3(0,8f,0);
    public Vector3 buttonInDistance = new Vector3(-0.2f,0,0);
    public Vector3 buttonOUTDistance = new Vector3(0, 0, -0.2f);
    public float duration = 5f;
    public bool open;
    Vector3 orginPos, endPos;
    Vector3 btnINOrginPos;
    Vector3 btnINEndPos;
    Vector3 btnOUTOrginPos;
    Vector3 btnOUTEndPos;

    private void Start()
    {
        orginPos = transform.position;
        endPos = transform.position + gateOpenDistance;

        btnINOrginPos = buttonIN.transform.position;
        btnINEndPos = buttonIN.transform.position + buttonInDistance;
        btnOUTOrginPos= buttonOUT.transform.position;
        btnOUTEndPos = buttonOUT.transform.position + buttonOUTDistance;

    }
    public void Update()
    {
        if (open)
        {
            DoorTrigger();
            open= false;
        }
    }
    public void DoorTrigger()
    {
        if (!isOpen)
        {
            StopAllCoroutines();
            StartCoroutine(DoorOpen(duration));
            isOpen = true;
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(DoorClose(duration));
            isOpen = false;
        }           
    }

    IEnumerator DoorOpen(float duration)
    {      
        float time = 0f;
        while (time < duration)
        {
            float t = Mathf.Pow(32, time - 1);
            transform.position = Vector3.Lerp(transform.position, endPos, t);
            buttonIN.transform.position = Vector3.Lerp(buttonIN.transform.position, btnINEndPos, t);
            buttonOUT.transform.position = Vector3.Lerp(buttonOUT.transform.position, btnOUTEndPos, t);
            time += Time.deltaTime / duration;
            yield return null;
        }
    }

    IEnumerator DoorClose(float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            float t = Mathf.Pow(32, time - 1);
            transform.position = Vector3.Lerp(transform.position, orginPos, t);
            buttonIN.transform.position = Vector3.Lerp(buttonIN.transform.position, btnINOrginPos, t);
            buttonOUT.transform.position = Vector3.Lerp(buttonOUT.transform.position, btnOUTOrginPos, t);
            time += Time.deltaTime / duration;
            yield return null;
        }
    }
}
