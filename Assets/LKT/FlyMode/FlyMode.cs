using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class FlyMode : MonoBehaviour
{
    DynamicMoveProvider dmp;
    TwoHandedGrabMoveProvider thgmp;
    GrabMoveProvider[] gmps;

    int modeEnteredCount = 0;
    bool flyMode;
    Vector3 previousPosition;

    public Vector3 FlyPositionOffset = new Vector3(0,10,0);
    public float goFlySpeed = 0.6f;
    public float backNormalSpeed = 0.6f;
    public float animateLerpExponentiationBase = 64;

    private void Start()
    {
        dmp = GetComponent<DynamicMoveProvider>();
        thgmp = GetComponent<TwoHandedGrabMoveProvider>();
        gmps = GetComponentsInChildren<GrabMoveProvider>();
    }

    private void Update()
    {
        if (modeEnteredCount == 1 && !flyMode)
        {
            Fly_Mode = true;
        }
        else if (modeEnteredCount == 0 && flyMode)
        {
            Fly_Mode = false;
        }
    }

    public void EnterFlyMode()
    {
        modeEnteredCount++;
    }

    public void ExitFlyMode()
    {
        modeEnteredCount--;

        if (modeEnteredCount < 0)
            modeEnteredCount = 0;
    }

    bool Fly_Mode
    {
        get
        {
            return flyMode;
        }
        set
        {
            if(value!=flyMode) {
                flyMode = value;
                if (value) //normal to god
                {
                    StopAllCoroutines();
                    StartCoroutine(GoFlyMode(goFlySpeed));
                }
                else //god to normal
                {
                    StopAllCoroutines();
                    StartCoroutine(BackNormalMode(backNormalSpeed));
                }
            }
        }
    }

    IEnumerator GoFlyMode(float animationDuration)
    {
        dmp.enableFly = true;
        dmp.useGravity = false;
        thgmp.useGravity = false;
        foreach (var g in gmps)
        {
            g.useGravity = false;
        }

        float time = 0;
        previousPosition = transform.position;
        while (time < 1)
        {
            float t = 1 - Mathf.Pow(animateLerpExponentiationBase, (1 - time) - 1);
            transform.position = Vector3.Lerp(previousPosition, FlyPositionOffset, t);

            time += Time.deltaTime / animationDuration;
            yield return null;
        }
        transform.position = FlyPositionOffset;
    }
    
    IEnumerator BackNormalMode(float animationDuration)
    {
        yield return null;

        float time = 0;
        Vector3 flyPosition = transform.position;
        while (time < 1)
        {
            float t = 1 - Mathf.Pow(animateLerpExponentiationBase, (1 - time) - 1);
            transform.position = Vector3.Lerp(flyPosition, previousPosition, t);

            time += Time.deltaTime / animationDuration;
            yield return null;
        }
        transform.position = previousPosition;

        dmp.enableFly = false;
        dmp.useGravity = true;
        thgmp.useGravity = true;
        foreach (var g in gmps)
        {
            g.useGravity = true;
        }

    }

}
