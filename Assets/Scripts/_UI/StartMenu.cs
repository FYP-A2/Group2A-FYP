using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using X = UnityEngine.InputSystem.InputAction.CallbackContext;

public class StartMenu : MonoBehaviour
{
    Custom_IA CIA;

    void Awake(){CIA = new Custom_IA();CIA.StartMenu.Enable();}
    void Start(){CIA.StartMenu.Any.performed += Any;}

    public void Any(X x){Any();} public void Any(){ SceneManager.LoadScene(1);CIA.StartMenu.Disable(); }
}
