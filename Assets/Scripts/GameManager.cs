using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class GameManager : MonoBehaviour
{
    private bool paused = false;
    private bool fastForward = false;
    private bool help = false;

    [Header("Fast Forward Options")]
    public float fastForwardTimeScale;
    public TextMeshProUGUI speedButtonText;

    [Header("Panel References")]
    public GameObject helpPanel;
    public GameObject pausePanel;

    public GameObject menuUIPanel;
    public GameObject gameUIPanel;
    public GameObject waveManager;

    [Header("Audio")]
    public AudioSource audioSource;

    [Header("Developer Mode")]
    public int developerModeSceneIndex = 1;

    [Header("System References")]
    public WaveManager2 wave;
    public StatsManager stats;

    [Header("Difficult Options")]
    public float easyWaveLength = 40f;
    public float hardWaveLength = 30f;
    public float hardBaseAS = 0.7f;

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

    public void StartGameEasy()
    {
        menuUIPanel.SetActive(false);
        gameUIPanel.SetActive(true);
        waveManager.SetActive(true);
        wave.WAVE_LENGTH = easyWaveLength;
    }

    public void StartGameHard()
    {
        menuUIPanel.SetActive(false);
        gameUIPanel.SetActive(true);
        waveManager.SetActive(true);
        wave.WAVE_LENGTH = hardWaveLength;
        stats.attackSpeed = hardBaseAS;
        stats.UpdateStatsAll();
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
            pausePanel.SetActive(false);
        }
        else
        {
            // Pause
            Time.timeScale = 0f;
            paused = true;
            pausePanel.SetActive(true);
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
            speedButtonText.text = ">>";

        }
        else
        {
            // Turn on fast forward
            if (paused) Time.timeScale = 0f;
            else Time.timeScale = fastForwardTimeScale;
            fastForward = true;
            speedButtonText.text = ">";
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

    public void DeveloperMode()
    {
        SceneManager.LoadScene(developerModeSceneIndex);
    }
}
