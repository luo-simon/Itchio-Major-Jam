using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;

    public int damage = 1;



    void Update()
    {
        
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Bullet collision...");

        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Hit enemy");
            other.GetComponent<EnemyManager>().TakeDamage(damage);

        }

        Destroy(gameObject);
    }
}
