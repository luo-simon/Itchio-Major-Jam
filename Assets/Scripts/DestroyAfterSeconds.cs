using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    public float seconds;

    void Update() => Invoke("Destroy", seconds);

    void Destroy() => Destroy(gameObject);
}
