using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    public StatsManager stats;

    // Price = BaseCost * Multiplier ^ (number of times upgraded)
    // Set in editor
    public int baseDamageCost;
    public float damageCostMultiplier;
    public float damageGrowth;

    public int baseAttackSpeedCost;
    public float attackSpeedMultiplier;
    public float attackSpeedGrowth;
    public float attackSpeedGrowthMultiplier;

    public int baseDefCost;
    public float defMultiplier;
    public float defGrowth;

    // Changed in code
    public int damageTier;
    public int damageCost;
    public int attackSpeedTier;
    public int attackSpeedCost;
    public int defTier;
    public int defCost;

    
    void Start()
    {
        damageCost = baseDamageCost;
        attackSpeedCost = baseAttackSpeedCost;
        defCost = baseDefCost;
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
        stats.IncreaseAttackSpeed(attackSpeedGrowth);

        attackSpeedTier++;
        attackSpeedCost = (int)(baseAttackSpeedCost * Mathf.Pow(attackSpeedMultiplier, attackSpeedTier));
        attackSpeedGrowth = stats.baseAttackSpeed * Mathf.Pow(attackSpeedGrowthMultiplier, attackSpeedTier);
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
    }

    private bool HasEnoughMoney(int cost)
    {
        if (stats.money >= cost)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
