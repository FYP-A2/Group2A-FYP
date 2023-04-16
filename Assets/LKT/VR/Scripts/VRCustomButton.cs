using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class VRCustomButton : MonoBehaviour
{
    [SerializeField]
    List<ExecutionGroup> egs = new List<ExecutionGroup>();

    [Serializable]
    public class ExecutionGroup
    {
        [SerializeField]
        public InputActionProperty input;
        [SerializeField]
        public UnityEvent unityEvent;
        [HideInInspector]
        public bool performedLast = false;
    }


    // Start is called before the first frame update
    private void OnEnable()
    {
        foreach (ExecutionGroup group in egs)
            group.input.action.Enable();
    }

    private void OnDisable()
    {
        foreach (ExecutionGroup group in egs)
            group.input.action.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (ExecutionGroup group in egs)
        {
            if (group.input.action.phase == InputActionPhase.Performed && !group.performedLast)
            {
                group.unityEvent.Invoke();
                group.performedLast = true;
            }

            if (group.input.action.phase != InputActionPhase.Performed)
                group.performedLast = false;
        }
    }
}
