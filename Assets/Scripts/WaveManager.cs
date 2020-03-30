using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class WaveManager : MonoBehaviour
{
    public int wave;
    public TextMeshProUGUI waveText;

    public float minTimeBtwSpawn;
    public float maxTimeBtwSpawns;
    public float timeToNextSpawn;

    public int budgetSpent = 0;
    public int budget;
    public float budgetMultiplier;

    public EnemyType[] enemies;

    void Start()
    {
        wave = 1;
        UpdateText();
    }

    void Update()
    {
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
                    budgetSpent += enemySpawn.GetComponent<EnemyManager>().budgetCost;
                    timeToNextSpawn = UnityEngine.Random.Range(minTimeBtwSpawn, maxTimeBtwSpawns);
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
        minTimeBtwSpawn = minTimeBtwSpawn * 0.8f;
        maxTimeBtwSpawns = minTimeBtwSpawn + 2f;
    }

    private void IncreaseBudget()
    {
        budgetSpent = 0;
        budget = (int) (budget * Mathf.Pow(budgetMultiplier, wave));
    }

    public void UpdateText()
    {
        waveText.text = "Wave " + wave;
    }
}
