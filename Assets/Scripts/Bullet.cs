﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;



    void Update()
    {
        
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;
    }
}
