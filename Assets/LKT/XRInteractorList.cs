using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRInteractorList : MonoBehaviour
{
    public static XRDirectInteractor directInteractorLeft;
    public static XRDirectInteractor directInteractorRight;

    public static XRBaseController baseControllerLeft;
    public static XRBaseController baseControllerRight;

    public XRDirectInteractor  _directInteractorLeft;
    public XRDirectInteractor _directInteractorRight;

    public XRBaseController _baseControllerLeft;
    public XRBaseController _baseControllerRight;

    // Start is called before the first frame update
    void Start()
    {
        directInteractorLeft = _directInteractorLeft;
        directInteractorRight = _directInteractorRight;
        baseControllerLeft = _baseControllerLeft;
        baseControllerRight = _baseControllerRight;
    }

    // Update is called once per frame
    void Update()
    {
        if (directInteractorLeft != null)
            Debug.Log("yes");
    }
}
