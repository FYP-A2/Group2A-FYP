using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextMove : MonoBehaviour { 

    private Vector3 randomNumber;
    public Text text;
    // Update is called once per frame
    void Start()
    {
        randomNumber = new Vector3(Random.Range(-1f, 1f) / 1000 * 2, Random.Range(0f, 1f) / 1000 * 2, Random.Range(-1f, 1f) / 1000 * 2);
    }
    void Update()
    {
        transform.position += randomNumber;
    }

    public void SetDamage(float damage)
    { 
        text.text = damage.ToString();
    }
}
