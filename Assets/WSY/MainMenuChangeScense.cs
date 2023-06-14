using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuChangeScense : MonoBehaviour
{
    public Text t;
    public void LoadScene(string s)
    {
        SceneManager.LoadScene(s);
        Debug.Log(s);
    }

    public void ChangeText(string s) { 
        t.text = s;
    }
        
}
