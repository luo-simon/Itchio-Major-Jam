using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName ="Enemy")]
public class EnemyType: ScriptableObject
{
    public string PrefabName;

    [Header("Stats")]
    public float moveSpeed;
    public int damage;
    public float maxHealth;

    [Space]
    public int waveStart;
    public int budgetCost;

   
    [HideInInspector] public int moneyDrop = 10;
    [HideInInspector] public Sprite sprite;

    [Header("Prefab")]
    public GameObject prefab;
}
