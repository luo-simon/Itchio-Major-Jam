using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    private bool paused = false;
    private bool fastForward = false;
    private bool help = false;

    public GameObject helpPanel;

    public float fastForwardTimeScale;

    [Header("Audio")]
    public AudioSource audioSource;

    void Start()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TogglePause();
        }
    }
    public void TogglePause()
    {
        audioSource.Play();

        if (paused)
        {
            // Unpause
            if (fastForward) Time.timeScale = fastForwardTimeScale;
            else Time.timeScale = 1f;
            paused = false;
        } else
        {
            // Pause
            Time.timeScale = 0f;
            paused = true;
        }
    }

    public void ToggleFastForward()
    {
        audioSource.Play();

        if (fastForward)
        {
            // Turn off fast forward
            if (paused) Time.timeScale = 0f;
            else Time.timeScale = 1f;
            fastForward = false;
        }
        else
        {
            // Turn on fast forward
            Time.timeScale = fastForwardTimeScale;
            fastForward = true;
        }
    }

    public void ToggleHelp()
    {
        audioSource.Play();

        if (help)
        {
            helpPanel.SetActive(false);
            help = false;
        }
        else
        {
            helpPanel.SetActive(true);
            help = true;
        }
    }
}
