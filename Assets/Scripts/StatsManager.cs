using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering.PostProcessing;
using System;

public class StatsManager : MonoBehaviour
{
    [Header("Object References")]
    public SentryController sentry;

    [Header("Damage")]
    public float baseDamage;
    [HideInInspector] public float damage;

    [Header("Attack Speed")]
    public float baseAttackSpeed;
    [HideInInspector] public float attackSpeed;

    [Header("Defense")]
    public int maxDef;
    [HideInInspector] public int currentDef;

    [Header("Other Stats")]
    public int enemiesKilled;
    public int money;

    [Header("UI/FX References")]
    public TextMeshProUGUI statsText;
    public TextMeshProUGUI defText;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI enemiesKilledText;
    public PostProcessVolume flashRed;
    public GameObject GameOverPanel;

    void Start()
    {
        currentDef = maxDef;
        damage = baseDamage;
        attackSpeed = baseAttackSpeed;
        enemiesKilled = 0;

        UpdateStatsAll();
    }

    public void TakeDamage(int amount)
    {
        currentDef -= amount;
        StartCoroutine("FlashRed");
        UpdateTexts();

        checkIfDead();
    }

    private void checkIfDead()
    {
        if (currentDef <= 0)
        {
            // No more defense - player has lost
            GameOverPanel.SetActive(true);
        }
    }

    IEnumerator FlashRed()
    {
        for (float weight = 0; weight < 1; weight += 0.2f)
        {
            flashRed.weight = weight;
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);

        for (float weight = 1; weight > 0; weight -= 0.1f)
        {
            flashRed.weight = weight;
            yield return null;
        }
    }
    

    public void AddMoney(int amount)
    {
        money += amount;
        UpdateTexts();
    }

    public void IncreaseDamage(float amount)
    {
        damage += amount;
        UpdateStatsAll();
    }

    public void IncreaseAttackSpeed(float amount)
    {
        attackSpeed += amount;
        UpdateStatsAll();
    }

    public void IncreaseDef(float amount)
    {
        maxDef += (int) amount;
        currentDef += (int)amount;
        UpdateStatsAll();
    }

    public void UpdateStatsAll()
    {
        UpdateTexts();
        UpdateSentryStats();
    }

    private void UpdateTexts()
    {
        defText.text = "<b>Defense:</b> " + currentDef + "/" + maxDef;
        enemiesKilledText.text = "Enemies Killed:<br>" + enemiesKilled;
        statsText.text = "Attack Speed: " + Mathf.Round(10 * attackSpeed)/10 + "<br>Damage: " + damage;
        moneyText.text = "<b>Money:</b> $" + money;
    }

    private void UpdateSentryStats()
    {
        sentry.attackSpeed = attackSpeed;
        sentry.damage = damage;
        sentry.attackCd = 1 / attackSpeed;
    }
}
