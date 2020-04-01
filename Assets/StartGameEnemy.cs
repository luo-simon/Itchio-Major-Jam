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
    public int difficulty;

    public void StartGame()
    {
        // Death FX
        cam.Shake();
        audioSource.Play();
        Instantiate(deathParticles, transform.position, Quaternion.identity);

        Debug.Log("Starting: with difficulty " + difficulty);
        gameManager.StartGameWithDifficulty(difficulty);

        // Destroy both "StartGameEnemy" enemies;
        Destroy(transform.parent.gameObject);
    }
}
