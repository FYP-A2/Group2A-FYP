using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class InputHandleExample : MonoBehaviour
{
    [SerializeField]
    InputActionProperty testAction1;

    [Header("Output")]
    public float outputFloat;
    public Vector2 outputVector2;
    public Vector3 outputVector3;
    public bool outputBool;


    private void OnEnable()
    {
        testAction1.EnableDirectAction();
    }
    private void OnDisable()
    {
        testAction1.DisableDirectAction();
    }


    void Start()
    {
        testAction1.action.performed += Action_performed;
    }

    private void Action_performed(InputAction.CallbackContext obj)
    {
        //do something when button was clicked
        Debug.Log("Action_performed");
    }

    void Update()
    {
        outputFloat = ReadValue(testAction1.action);
        outputVector2 = ReadValueVector2(testAction1.action);
        outputVector3 = ReadValueVector3(testAction1.action);
        outputBool = ReadValueBool(testAction1.action);
    }


    private float ReadValue(InputAction action)
    {
        if (action == null)
            return default;

        if (action.activeControl is AxisControl)
            return action.ReadValue<float>();

        if (action.activeControl is Vector2Control)
            return action.ReadValue<Vector2>().magnitude;

        if (action.activeControl is Vector3Control)
            return action.ReadValue<Vector3>().magnitude;

        return action.phase == InputActionPhase.Performed ? 1f : 0f;
    }

    private Vector2 ReadValueVector2(InputAction action)
    {
        if (action == null)
            return default;

        if (action.activeControl is Vector2Control )
            return action.ReadValue<Vector2>();

        return default;
    }

    private Vector3 ReadValueVector3(InputAction action)
    {
        if (action == null)
            return default;

        if (action.activeControl is Vector3Control)
            return action.ReadValue<Vector3>();

        return default;
    }

    private bool ReadValueBool(InputAction action)
    {
        return ReadValue(action) != 0;
    }
}
