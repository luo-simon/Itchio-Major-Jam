using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int maxHealth;
    private int currentHealth;

    public float moveSpeed;
    public int damage;

    public int moneyDrop = 10;

    public bool hasReachedWall = false;

    // Rendering
    public Color hitColor;
    private SpriteRenderer sprite;

    void Start()
    {
        currentHealth = maxHealth;
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!hasReachedWall) Move();

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Move()
    {
        if (transform.position.z >= 1f)
            transform.position += new Vector3(0, 0, -moveSpeed * Time.deltaTime);
        else
            hasReachedWall = true;
    }

    public void TakeDamage(int damage)
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
