using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WaveManager2 : MonoBehaviour
{
    [Header("Wave Info (Runtime)")]
    public int wave;
    public bool waiting = false;

    [Header("Wave Generation Settings")]
    public float WAVE_LENGTH = 40f;
    public int STARTING_BUDGET = 20;
    public int BUDGET_INCREASE = 10;
    [SerializeField] private int budget;

    [Header("UI/FX References")]
    public GameObject spawnFX;
    public TextMeshProUGUI waveText;
    public Image waveProgressBar;

    [Header("Enemies")]
    public EnemyType[] enemies;

    [Header("Private Spawn Values")]
    [SerializeField] private List<EnemyType> availableEnemies = new List<EnemyType>();
    // Spawn Values: 
    [SerializeField] private float spawnCd;
    [SerializeField] private float currentSpawnCd;
    [SerializeField] private float waveBudget;

    void Start()
    {
        // Initialise values
        wave = 1;
        budget = STARTING_BUDGET;
        UpdateText();
        UpdateAvailableEnemies();
        UpdateSpawnValues();
    }

    
    void Update()
    {
        UpdateProgressBar();

        if (waiting) return;

        SpawnWave();
    }

    private void SpawnWave()
    {
        if (budget >= 0)
        {
            if (currentSpawnCd <= 0)
            {
                // Choose random unit from available enemies
                EnemyType chosenEnemy = availableEnemies[(int)Random.Range(0, availableEnemies.Count)];
                // Spawn the prefab of the chosen unit
                GameObject enemySpawn = Instantiate(
                    chosenEnemy.prefab,
                    new Vector3(UnityEngine.Random.Range(-2.5f, 2.5f), 0.5f, 10f), // Spawns at random x value, and set distance away from player
                    Quaternion.identity);
                // Instantiate particle effects
                Instantiate(spawnFX, new Vector3(enemySpawn.transform.position.x, 0.5f, enemySpawn.transform.position.z - 1f), Quaternion.identity);
                // Take away cost from budget
                budget -= chosenEnemy.budgetCost;
                // Set spawn cooldown
                currentSpawnCd = spawnCd;
            } else
            {
                currentSpawnCd -= Time.deltaTime;
            }
        } else // All of budget has been spend
        {
            Debug.Log("Next wave...");
            StartCoroutine("NextWaveAfterWait");
        }
    }

    IEnumerator NextWaveAfterWait()
    {
        Debug.Log("Waiting...");

        waiting = true;
        yield return new WaitForSeconds(5f);

        wave++;
        UpdateText();
        UpdateSpawnValues();
        UpdateAvailableEnemies();

        waiting = false;
    }

    private void UpdateAvailableEnemies()
    {
        availableEnemies.Clear();
        foreach (EnemyType enemy in enemies)
        {
            if (enemy.waveStart <= wave)
            {
                // Add to available enemies list
                availableEnemies.Add(enemy);
                // If the length of list is 5 (it should max be 4) then delete first element
                if (availableEnemies.Count >= 5) availableEnemies.RemoveAt(0);
            }
        }

            
    }

    private void UpdateSpawnValues()
    {
        // Budget is linear and can be calculated by the following:
        budget = BUDGET_INCREASE * (wave - 1) + STARTING_BUDGET;
        waveBudget = budget;

        // Units to spawn is: budget/average unit cost (which is just the no. available enemies + 1 due to the cost and waveStart of units)
        int unitsToSpawn = budget / (availableEnemies.Count + 1);
        Debug.Log(unitsToSpawn + " units to spawn in this wave");

        // SpawnCd is: the length of the wave divided by the no. of units to be spawned
        spawnCd = WAVE_LENGTH / unitsToSpawn;
    }

    private void UpdateProgressBar()
    {
        waveProgressBar.fillAmount = Mathf.Lerp(waveProgressBar.fillAmount, (waveBudget - budget) / waveBudget, Time.deltaTime * 3);
    }

    private void UpdateText()
    {
        waveText.text = "Wave " + wave;
    }
}
