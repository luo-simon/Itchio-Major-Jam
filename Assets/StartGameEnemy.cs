using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameEnemy : MonoBehaviour
{
    [Header("Audio/FX")]
    public AudioSource audioSource;
    public CameraShake cam;
    public GameObject deathParticles;

    [Header("References")]
    public GameManager gameManager;

    [Header("Options")]
    public bool hardModeStart;

    public void StartGame()
    {
        // Death FX
        cam.Shake();
        audioSource.Play();
        Instantiate(deathParticles, transform.position, Quaternion.identity);

        if (hardModeStart)
        {
            // Start in hard more
            Debug.Log("Starting: HARD");
            gameManager.StartGameHard();
        } else
        {
            // Start in normal mode
            Debug.Log("Starting: NORMAL");
            gameManager.StartGameEasy();
        }

        // Destroy both "StartGameEnemy" enemies;
        Destroy(transform.parent.gameObject);
    }
}
