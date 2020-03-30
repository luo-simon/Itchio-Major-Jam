using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;

    public GameObject bulletImpact;

    [HideInInspector] public float damage = 1f;


    void Update()
    {
        
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyManager>().TakeDamage(damage);
        }

        Instantiate(bulletImpact, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
