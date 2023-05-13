using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameMsg : MonoBehaviour
{
    public Text text;
    string msg;

    // Start is called before the first frame update
    void Start()
    {
        msg = PlayerPrefs.GetString("msg", "lose");
        if (msg == "lose")
            text.text = "Defeat";
        else if (msg == "win")
            text.text = "Victory";
    }
}
