using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class angularSpeed : MonoBehaviour
{
    public float prevDistance = 0;
    public float currentDistance = 0;
    public float space = 1;
    public float angle;
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        prevDistance = currentDistance;
        currentDistance = Vector2.Distance(transform.position, new Vector3(0, 0, transform.position.z));
        if (prevDistance == 0) prevDistance = currentDistance;
        angle = Mathf.Asin((2 * space) / (currentDistance * prevDistance));
    }
}
