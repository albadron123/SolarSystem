using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elipceOrbit : MonoBehaviour
{
    [SerializeField]
    GameObject orbitLine;

    public float excentricity;
    public float semiMayorAxis;
    float semiMinorAxis;
    float focalParam;
    public float angle;
    public float newAngle = 90;
    public float angSpeed;
    public float linearSpeed;

    void Start()
    {
        semiMinorAxis = semiMayorAxis * Mathf.Sqrt(1 - Mathf.Pow(excentricity, 2));
        focalParam = Mathf.Pow(semiMinorAxis, 2) / semiMayorAxis;

        GameObject orbitLineInstance = Instantiate(orbitLine, Vector3.zero, Quaternion.identity);
        DrawEllipse drawEllipse = orbitLineInstance.GetComponent<DrawEllipse>();
        drawEllipse.xradius = semiMayorAxis;
        drawEllipse.yradius = semiMinorAxis;
        orbitLineInstance.transform.rotation = Quaternion.Euler(new Vector3(0, 0, newAngle));
        drawEllipse.Draw();
        angle = 0;
        
    }

    
    void Update()
    {
        semiMinorAxis = semiMayorAxis * Mathf.Sqrt(1 - Mathf.Pow(excentricity, 2));
        focalParam = Mathf.Pow(semiMinorAxis, 2) / semiMayorAxis;

        float radius = focalParam / (1 + excentricity * Mathf.Cos(Mathf.Deg2Rad * angle));
        

        angSpeed = linearSpeed / Vector2.Distance(transform.position, Vector2.zero);
        angle += angSpeed*Time.deltaTime;

        float xPosition = radius * Mathf.Cos(Mathf.Deg2Rad * angle);
        float yPosition = radius * Mathf.Sin(Mathf.Deg2Rad * angle);
        transform.position = new Vector3(xPosition*Mathf.Cos(newAngle) - yPosition*Mathf.Sin(newAngle), yPosition * Mathf.Cos(newAngle) + xPosition * Mathf.Sin(newAngle), transform.position.z);
    }

}
