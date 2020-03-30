using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName ="Enemy")]
public class EnemyType: ScriptableObject
{
    public string PrefabName;

    public float maxHealth;
    public float moveSpeed;
    public int damage;
    public int moneyDrop = 10;

    public Sprite sprite;

    public int waveStart;
    public int budgetCost;

    public GameObject prefab;
}
