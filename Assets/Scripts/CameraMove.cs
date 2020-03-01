using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x + Input.GetAxisRaw("Horizontal") * Time.deltaTime,
                                         transform.position.y + Input.GetAxisRaw("Vertical") * Time.deltaTime,
                                         transform.position.z);
    }
}
