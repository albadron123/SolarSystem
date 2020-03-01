using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectManager : MonoBehaviour
{
    [SerializeField]
    GameObject planet;

    [SerializeField]
    GameObject eccentricityText;
    [SerializeField]
    GameObject semimajorAxisText;
    [SerializeField]
    GameObject longitudeOfTheAscendingNodeText;
    [SerializeField]
    GameObject argumentOfPeriapsisText;
    [SerializeField]
    GameObject trueAnomalyText;

    void Start()
    {
        
    }

    void Update()
    {
             
    }

    public void AddNewObjectFromInput()
    {
        float eccentricity = float.Parse(eccentricityText.GetComponent<Text>().text);
        float semimajorAxis = float.Parse(semimajorAxisText.GetComponent<Text>().text);
        float longitudeOfTheAscendingNode = float.Parse(longitudeOfTheAscendingNodeText.GetComponent<Text>().text);
        float argumentOfPeriapsis = float.Parse(argumentOfPeriapsisText.GetComponent<Text>().text);
        float trueAnomaly = float.Parse(trueAnomalyText.GetComponent<Text>().text);
        AddNewObject(eccentricity, semimajorAxis, longitudeOfTheAscendingNode, argumentOfPeriapsis, trueAnomaly);
    }

    public void AddNewObject(float eccentricity,
                             float semimajorAxis,
                             float longitudeOfTheAscendingNode,
                             float argumentOfPeriapsis,
                             float trueAnomaly)
    {
        if (eccentricity == 1) return;
        if (eccentricity < 0) return;
        if (semimajorAxis <= 0) return;
        GameObject planetInstance = Instantiate(planet, new Vector3(0, 2* semimajorAxis, 0), Quaternion.identity);
        planetInstance.GetComponent<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value);
        Planet planetScript = planetInstance.GetComponent<Planet>();
        planetScript.eccentricity = eccentricity;
        planetScript.semimajorAxis = semimajorAxis;
        planetScript.longitudeOfTheAscendingNode = longitudeOfTheAscendingNode;
        planetScript.argumentOfPeriapsis = argumentOfPeriapsis;
        planetScript.trueAnomaly = trueAnomaly;
    }
}
