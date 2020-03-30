using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Billboard : MonoBehaviour
{
    public bool inverse = true;

    void Update()
    {
        transform.LookAt(Camera.main.transform.position, Vector3.up);
        if (inverse) transform.Rotate(new Vector3(0, 180));
    }
}
