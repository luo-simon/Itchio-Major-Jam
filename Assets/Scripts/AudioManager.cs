using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource source;

    void Start()
    {
        if (GameObject.FindGameObjectsWithTag("Audio").Length == 1)
        {
            DontDestroyOnLoad(transform.gameObject);
            source.Play();
        }
        else
        {
            Destroy(gameObject);
        }

    }
}
