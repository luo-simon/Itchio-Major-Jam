using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;

public class UpgradesManager2 : MonoBehaviour
{
    [Header("StatsManager Reference")]
    public StatsManager stats;

    [Header("Sentry Tier")]
    public int evolveCostT2;
    public int evolveCostT3;

    // Set in editor
    [Header("Damage Settings")]
    public int baseDamageCost;
    public float damageCostIncrease;
    public float damageGrowth;

    [Header("Attack Speed Settings")]
    public int baseAttackSpeedCost;
    public float attackSpeedCostIncrease;
    public float attackSpeedGrowth;

    [Header("Defense Settings")]
    public int baseDefCost;
    public float defCostIncrease;
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

    private bool waiting = false;

    void Start()
    {
        damageCost = baseDamageCost;
        attackSpeedCost = baseAttackSpeedCost;
        defCost = baseDefCost;
        sentryTier = stats.sentry.currentTier;
        UpdateTexts();
    }

    void Update()
    {
        if (waiting) return;
        // Upgrade Hotkeys
        if (Input.GetKey(KeyCode.Q))
        {
            // Upgrade DEFENSE button
            UpgradeDef();
            waiting = true;
            // Wait for 0.1 secs before getting keyboard input
            Invoke("ResetWaiting", 0.5f);
        }
        if (Input.GetKey(KeyCode.W))
        {
            // Upgrade AS button
            UpgradeAttackSpeed();
            waiting = true;
            // Wait for 0.1 secs before getting keyboard input
            Invoke("ResetWaiting", 0.5f);
        }
        if (Input.GetKey(KeyCode.E))
        {
            // Upgrade DAMAGE button
            UpgradeDamage();
            waiting = true;
            // Wait for 0.1 secs before getting keyboard input
            Invoke("ResetWaiting", 0.5f);
        }

        
        
    }

    private void ResetWaiting()
    {
        waiting = false;
    }

    public void UpgradeTier()
    {
        // Check if enough money
        if (!HasEnoughMoney(evolveCostT2))
        {
            Debug.Log("Not enough money");
            return;
        }

       

        // Return if already at max
        if (sentryTier >= 2)
        {
            return;
        } else
        {
            audioSource.PlayOneShot(evolve);

            stats.AddMoney(-evolveCostT2);
            stats.sentry.Evolve();

            sentryTier = stats.sentry.currentTier;

            evolveCostT2 = evolveCostT3;
            UpdateTexts();
        }

        
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
        damageCost = (int)(damageCostIncrease * damageTier + baseDamageCost);

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
        stats.IncreaseAttackSpeed(attackSpeedGrowth * (sentryTier + 1));

        attackSpeedTier++;
        attackSpeedCost = (int)(attackSpeedCostIncrease * attackSpeedTier + baseAttackSpeedCost);

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
        defCost = (int)(defCostIncrease * defTier + baseDefCost);

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
        moreStats.text = "Damage Tier : " + damageTier + "<br>Atk. Speed Tier : " + attackSpeedTier + "<br>Damage/sec: " + Mathf.Round(stats.damage * stats.attackSpeed * 10) / 10;
        sentryTierText.text = "Upgrade Tier <br> <i>Current Tier: " + (sentryTier + 1);
        if (sentryTier >= 2)
        {
            sentryTierCostText.text = "MAX";
        } else
        {
            sentryTierCostText.text = "$" + evolveCostT2;
        }
    }
}
