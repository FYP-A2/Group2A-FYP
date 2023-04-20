using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRandomPlayer : MonoBehaviour
{
    AudioSource audioSource;
    public List<AudioClip> clipList;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.playOnAwake = false;
    }

    public void Play(int n = -1)
    {
        if (n == -1)
        {
            audioSource.clip = clipList[Random.Range(0,clipList.Count)];
        }
        else if (n< clipList.Count)
        {
            audioSource.clip = clipList[n];
        }
        audioSource.Play();
    }
}
