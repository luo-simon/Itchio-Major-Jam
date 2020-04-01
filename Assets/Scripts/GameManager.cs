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
    public UpgradesManager2 upgrades;

    [Header("Difficult Options")]
    public float easyWaveLength = 40f;
    public float hardWaveLength;
    public float veryHardWaveLength;

    public float easyBaseAS = 1f;
    public float hardBaseAS = 0.7f;
    public float veryHardBaseAS = 0.8f;

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

    public void StartGameWithDifficulty(int difficulty)
    {
        menuUIPanel.SetActive(false);
        gameUIPanel.SetActive(true);
        waveManager.SetActive(true);

        if (difficulty == 1) {
            wave.WAVE_LENGTH = easyWaveLength;
            stats.attackSpeed = easyBaseAS; 
            upgrades.evolveCostT2 = 200;
            upgrades.evolveCostT3 = 400;

            upgrades.baseDefCost = 5;
            upgrades.defCost = 5;
            upgrades.defCostIncrease = 2;

            upgrades.baseDamageCost = 10;
            upgrades.damageCost = 10;
            upgrades.damageCostIncrease = 3;

            upgrades.baseAttackSpeedCost = 5;
            upgrades.attackSpeedCost = 5;
            upgrades.attackSpeedCostIncrease = 5;

            upgrades.UpdateTexts();
        }
        else if (difficulty == 3) { wave.WAVE_LENGTH = hardWaveLength; stats.attackSpeed = hardBaseAS; }
        else if (difficulty == 4)
        {
            wave.WAVE_LENGTH = veryHardWaveLength;
            stats.attackSpeed = veryHardBaseAS;
            upgrades.baseDefCost = 10;
            upgrades.defCost = 10;
            upgrades.defCostIncrease = 5;

            upgrades.baseDamageCost = 12;
            upgrades.damageCost = 12;

            upgrades.evolveCostT2 = 300;
            upgrades.evolveCostT3 = 600;

            upgrades.UpdateTexts();
        }
        
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

    public void RestartScene()
    {
        SceneManager.LoadScene(0);
    }
}
