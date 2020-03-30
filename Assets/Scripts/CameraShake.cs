using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // How long the object should shake for.
    public float shakeDuration;
    public float startShakeDuration;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.3f;

    Vector3 originalPos;

    // Testing purposes
    public bool testShake;

    void Start()
    {
        originalPos = transform.position;
    }

    void Update()
    {
        if (testShake) { Shake(); testShake = false; }

        if (shakeDuration > 0)
        {
            transform.position = originalPos + Random.insideUnitSphere * shakeAmount;
            shakeDuration -= Time.deltaTime;
        }
        else
        {
            transform.position = originalPos;
            shakeDuration = 0f;
        }
    }

    public void Shake()
    {
        shakeDuration = startShakeDuration;
    }
}
