using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{

    public float eccentricity;
    public float semimajorAxis;
    float semiminorAxis;
    public float longitudeOfTheAscendingNode;
    public float argumentOfPeriapsis;
    public float trueAnomaly;
    public float angle = 0;
    public float deltaAngle = 0;
    float focalParam;
    public float linearSpeed = 0.5f;

    float time = 0.3f;
    float timer = 0;

    float currentDistance;
    float prevDistance;

    [SerializeField]
    GameObject arrow;
    [SerializeField]
    GameObject orbitLine;
    [SerializeField]
    GameObject dot;

    void Start()
    {
        Instantiate(arrow, Vector3.zero, Quaternion.Euler(new Vector3(0, 0, longitudeOfTheAscendingNode)));
        Instantiate(arrow, Vector3.zero, Quaternion.Euler(new Vector3(0, 0, longitudeOfTheAscendingNode + argumentOfPeriapsis)));

        semiminorAxis = semimajorAxis * Mathf.Sqrt(1 - Mathf.Pow(eccentricity, 2));
        focalParam = Mathf.Pow(semiminorAxis, 2) / semimajorAxis;

        //positions with angle 0
        float xPosition = -(semimajorAxis * eccentricity);
        float yPosition = 0;
        float rotation = longitudeOfTheAscendingNode + argumentOfPeriapsis;
        GameObject orbitLineInstance = Instantiate(orbitLine, 
                                                   new Vector3(xPosition * Mathf.Cos(rotation * Mathf.Deg2Rad) - yPosition * Mathf.Sin(rotation * Mathf.Deg2Rad),
                                                               yPosition * Mathf.Cos(rotation * Mathf.Deg2Rad) + xPosition * Mathf.Sin(rotation * Mathf.Deg2Rad),
                                                               0), 
                                                   Quaternion.identity);

        DrawEllipse drawEllipse = orbitLineInstance.GetComponent<DrawEllipse>();
        drawEllipse.xradius = semimajorAxis;
        drawEllipse.yradius = semiminorAxis;
        orbitLineInstance.transform.rotation = Quaternion.Euler(new Vector3(0, 0, longitudeOfTheAscendingNode + argumentOfPeriapsis));
        drawEllipse.Draw();
        linearSpeed = Mathf.PI * semimajorAxis * semiminorAxis / 1000;
        angle = 0;
    }

    void FixedUpdate()
    {
        if (prevDistance == 0) prevDistance = Vector3.Distance(gameObject.transform.position, new Vector3(0, 0, transform.position.z));
        else prevDistance = currentDistance;

        if (timer < time) timer += Time.deltaTime;
        else
        {
            timer = 0;
            //Instantiate(dot, transform.position, Quaternion.identity);
        }
        



        float radius = focalParam / (1 + eccentricity * Mathf.Cos(Mathf.Deg2Rad * angle));
        float xPosition = radius * Mathf.Cos(Mathf.Deg2Rad * angle);
        float yPosition = radius * Mathf.Sin(Mathf.Deg2Rad * angle);
        float rotation = longitudeOfTheAscendingNode + argumentOfPeriapsis;
        transform.position = new Vector3(xPosition * Mathf.Cos(rotation * Mathf.Deg2Rad) - yPosition * Mathf.Sin(rotation * Mathf.Deg2Rad),
                                         yPosition * Mathf.Cos(rotation * Mathf.Deg2Rad) + xPosition * Mathf.Sin(rotation * Mathf.Deg2Rad),
                                         transform.position.z);
        //transform.position = new Vector3(xPosition, yPosition, transform.position.z);
        currentDistance = Vector2.Distance(transform.position, new Vector3(0, 0, transform.position.z));
        deltaAngle = Mathf.Rad2Deg * Mathf.Asin((2 * linearSpeed) / (currentDistance * prevDistance));
        angle += deltaAngle;
    }

}
