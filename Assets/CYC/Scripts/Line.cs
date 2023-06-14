using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private Material m;
    [SerializeField] private float offsetSpeed = -0.005f;
    Vector2 offset;
    // Start is called before the first frame update
    void Start()
    {
       m = GetComponent<LineRenderer>().material;
        offset = Vector2.zero;
        StartCoroutine(Move());
    }

    // Update is called once per frame
    void Update()
    {
        m.mainTextureOffset = offset;
        offset += new Vector2(offsetSpeed, 0f);
    }

    IEnumerator Move()
    {
        yield return null;
    }
}
