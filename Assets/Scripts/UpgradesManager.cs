using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;

public class UpgradesManager : MonoBehaviour
{
    [Header("StatsManager Reference")]
    public StatsManager stats;

    [Header("Sentry Tier")]
    public int evolveCostT2;
    public int evolveCostT3;

    // Price = BaseCost * Multiplier ^ (number of times upgraded)
    // Set in editor
    [Header("Damage Settings")]
    public int baseDamageCost;
    public float damageCostMultiplier;
    public float damageGrowth;

    [Header("Attack Speed Settings")]
    public int baseAttackSpeedCost;
    public float attackSpeedMultiplier;
    public float attackSpeedGrowth;
    public float attackSpeedGrowthMultiplier;

    [Header("Defense Settings")]
    public int baseDefCost;
    public float defMultiplier;
    public float defGrowth;

    [Header("Runtime Values")]
    public int sentryTier;
    public int damageTier;
    public int damageCost;
    public int attackSpeedTier;
    public int attackSpeedCost;
    public int defTier;
    public int defCost;

    [Header("Text UIs")]
    public TextMeshProUGUI damageCostText;
    public TextMeshProUGUI attackSpeedCostText;
    public TextMeshProUGUI defCostText;
    public TextMeshProUGUI moreStats;
    public TextMeshProUGUI sentryTierCostText;
    public TextMeshProUGUI sentryTierText;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip buy;
    public AudioClip error;
    public AudioClip evolve;


    void Start()
    {
        damageCost = baseDamageCost;
        attackSpeedCost = baseAttackSpeedCost;
        defCost = baseDefCost;
        sentryTier = stats.sentry.currentTier;
        UpdateTexts();
    }

    public void UpgradeTier()
    {
        // Check if enough money
        if (!HasEnoughMoney(evolveCostT2))
        {
            Debug.Log("Not enough money");
            return;
        }

        audioSource.PlayOneShot(evolve);

        stats.AddMoney(-evolveCostT2);
        stats.sentry.Evolve();

        sentryTier = stats.sentry.currentTier;
        evolveCostT2 = evolveCostT3;

        UpdateTexts();
    }

    public void UpgradeDamage()
    {
        // Check if enough money
        if (!HasEnoughMoney(damageCost))
        {
            Debug.Log("Not enough money");
            return;
        }

        stats.AddMoney(-damageCost);
        stats.IncreaseDamage(damageGrowth);

        damageTier++;
        damageCost = (int) (baseDamageCost * Mathf.Pow(damageCostMultiplier, damageTier));

        UpdateTexts();
    }

    public void UpgradeAttackSpeed()
    {
        // Check if enough money
        if (!HasEnoughMoney(attackSpeedCost))
        {
            Debug.Log("Not enough money");
            return;
        }

        stats.AddMoney(-attackSpeedCost);
        stats.IncreaseAttackSpeed(attackSpeedGrowth * (sentryTier+1));

        attackSpeedTier++;
        attackSpeedCost = (int)(baseAttackSpeedCost * Mathf.Pow(attackSpeedMultiplier, attackSpeedTier));
        attackSpeedGrowth = stats.baseAttackSpeed * Mathf.Pow(attackSpeedGrowthMultiplier, attackSpeedTier);

        UpdateTexts();
    }

    public void UpgradeDef()
    {
        // Check if enough money
        if (!HasEnoughMoney(defCost))
        {
            Debug.Log("Not enough money");
            return;
        }

        stats.AddMoney(-defCost);
        stats.IncreaseDef(defGrowth);

        defTier++;
        defCost = (int)(baseDefCost * Mathf.Pow(defMultiplier, defTier));

        UpdateTexts();
    }

    private bool HasEnoughMoney(int cost)
    {
        if (stats.money >= cost)
        {
            audioSource.PlayOneShot(buy);
            return true;
        }
        else
        {
            audioSource.PlayOneShot(error);
            return false;
        }
    }

    public void UpdateTexts()
    {
        damageCostText.text = "$" + damageCost;
        attackSpeedCostText.text = "$" + attackSpeedCost;
        defCostText.text = "$" + defCost;
        moreStats.text = "Damage Tier : " + damageTier + "<br>Atk. Speed Tier : " + attackSpeedTier + "<br>Damage/sec: " + Mathf.Round(stats.damage * stats.attackSpeed * 10)/10;
        sentryTierCostText.text = "$" + evolveCostT2;
        sentryTierText.text = "Upgrade Tier <br> <i>Current Tier: " + (sentryTier + 1);
    }
}
