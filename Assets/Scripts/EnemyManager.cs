using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public EnemyType enemy;
    
    // Set by scriptable object
    [HideInInspector] public float maxHealth;
    [SerializeField ] private float currentHealth;
    [HideInInspector] public float moveSpeed;
    [HideInInspector] public int damage;
    [HideInInspector] public int moneyDrop = 10;
    [HideInInspector] public int budgetCost;

    public bool hasReachedWall = false;
    public GameObject deathParticles;

    // Universal attack speeds
    public float startAttackCd = 1f;
    private float attackCd = 0f;

    // Rendering
    public Color hitColor;
    private SpriteRenderer sprite;
    private Animator animator;
    private Animation anim;

    // FX
    private CameraShake cam;

    // Other
    private StatsManager stats;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        animator= GetComponent<Animator>();
        anim = GetComponent<Animation>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
        stats = GameObject.FindGameObjectWithTag("StatsManager").GetComponent<StatsManager>();
        Initialise();
    }

    void Initialise()
    {
        maxHealth = enemy.maxHealth;
        moveSpeed = enemy.moveSpeed;
        damage = enemy.damage;
        moneyDrop = enemy.moneyDrop;
        sprite.sprite = enemy.sprite;
        budgetCost = enemy.budgetCost;

        currentHealth = maxHealth;
    }

    void Update()
    {
        if (!hasReachedWall) Move();
        else DamageWall();

        if (currentHealth <= 0)
        {
            RunDeathSequence();
        }
    }

    private void DamageWall()
    {
        if (attackCd <= 0)
        {
            stats.TakeDamage(damage);
            attackCd = startAttackCd;
        } else
        {
            attackCd -= Time.deltaTime;   
        }
        
    }

    private void RunDeathSequence()
    {
        // Add money
        stats.AddMoney(moneyDrop);
        stats.enemiesKilled++; stats.UpdateStatsAll();
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        cam.Shake();
        Destroy(gameObject);
    }

    private void Move()
    {
        if (transform.position.z >= 1f)
            transform.position += new Vector3(0, 0, -moveSpeed * Time.deltaTime);
        else
            hasReachedWall = true;
    }

    public void TakeDamage(float damage)
    {
        StartCoroutine("Flash");
        currentHealth -= damage;
    }

    IEnumerator Flash()
    {
        sprite.color = hitColor;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }
}
