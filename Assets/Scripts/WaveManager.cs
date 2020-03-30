using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class WaveManager : MonoBehaviour
{
    public int wave;
    public TextMeshProUGUI waveText;
    public Image waveProgressBar;

    public float startSpawnCd = 5f;
    private float spawnCd;
    public float startSpawnInterval = 2f;
    private float spawnInterval;
    public float timeToNextSpawn = 0f;

    public int budgetSpent = 0;
    public int startBudget;
    public int budget;
    public float budgetMultiplier;

    public EnemyType[] enemies;

    void Start()
    {
        wave = 1;
        startBudget = budget;
        spawnCd = startSpawnCd;
        spawnInterval = startSpawnInterval;
        UpdateText();
    }

    void Update()
    {
        waveProgressBar.fillAmount = Mathf.Lerp(
            waveProgressBar.fillAmount,
            (float)budgetSpent / (float)budget,
            Time.deltaTime * 3f);

        if (budgetSpent < budget)
        {
            if (timeToNextSpawn <= 0)
            {
                int i = UnityEngine.Random.Range(0, enemies.Length);
                if(enemies[i].waveStart <= wave)
                {
                    GameObject enemySpawn = Instantiate(
                    enemies[i].prefab,
                    new Vector3(UnityEngine.Random.Range(-2.5f, 2.5f), 0.5f, 10f),
                    Quaternion.identity);

                    int cost = enemySpawn.GetComponent<EnemyManager>().budgetCost;
                    budgetSpent += cost;
                    timeToNextSpawn = UnityEngine.Random.Range(spawnCd, spawnCd + spawnInterval) * ((i+1f)/(wave/2f));
                    Debug.Log(wave/2);
                }
            } else
            {
                timeToNextSpawn -= Time.deltaTime;
            }
        } else
        {
            Debug.Log("Next WAVE...");
            wave++;
            UpdateText();
            IncreaseBudget();
            DecreaseSpawnCd();
        }
    }

    private void DecreaseSpawnCd()
    {
        spawnCd = startSpawnCd * Mathf.Pow(0.95f, wave) + (Mathf.Sin(2*wave) + 1)/2;
        spawnInterval = startSpawnInterval * Mathf.Pow(0.98f, wave) + 0.1f;
    }

    private void IncreaseBudget()
    {
        budgetSpent = 0;
        budget = (int) (startBudget * Mathf.Pow(budgetMultiplier, wave));
    }

    public void UpdateText()
    {
        waveText.text = "Wave " + wave;
    }
}
