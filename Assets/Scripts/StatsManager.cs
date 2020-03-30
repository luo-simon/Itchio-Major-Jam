using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsManager : MonoBehaviour
{
    public SentryController sentry;
    public UpgradesManager upgrades;

    public int maxDef;
    [HideInInspector] public int currentDef;
    public TextMeshProUGUI defText;

    public float baseDamage;
    [HideInInspector] public float damage;

    public float baseAttackSpeed;
    [HideInInspector] public float attackSpeed;

    public TextMeshProUGUI statsText;

    public int enemiesKilled;
    public TextMeshProUGUI enemiesKilledText;

    public int money;
    public TextMeshProUGUI moneyText;

    void Start()
    {
        currentDef = maxDef;
        damage = baseDamage;
        attackSpeed = baseAttackSpeed;
        enemiesKilled = 0;

        UpdateStatsAll();
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
