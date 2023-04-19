using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateButton : MonoBehaviour
{
    public GameObject gate;
    public bool isOpen = false;
    public Vector3 gateOpenDistance;
    public Vector3 buttonInDistance;
    public float duration;
    public bool open;
    Vector3 gateOrginPos, gateEndPos, orginPos, endPos;

    private void Start()
    {
        orginPos= transform.position;
        endPos = transform.position + buttonInDistance;
        gateOrginPos = gate.transform.position;
        gateEndPos = gate.transform.position + gateOpenDistance;
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
            gate.transform.position = Vector3.Lerp(gate.transform.position, gateEndPos, t);
            transform.position = Vector3.Lerp(transform.position, endPos, t);
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
            gate.transform.position = Vector3.Lerp(gate.transform.position, gateOrginPos, t);
            transform.position = Vector3.Lerp(transform.position, orginPos, t);
            time += Time.deltaTime / duration;
            yield return null;
        }
    }
}
