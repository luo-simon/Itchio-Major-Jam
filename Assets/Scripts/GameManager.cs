using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool paused = false;
    private bool fastForward = false;

    public float fastForwardTimeScale;

    public void TogglePause()
    {
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
}
