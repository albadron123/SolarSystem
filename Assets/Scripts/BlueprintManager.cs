using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class BlueprintManager : MonoBehaviour
{
    [SerializeField]
    GameObject pathInputField;

    bool inputIsActive = false;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inputIsActive = !inputIsActive;
            if (inputIsActive) pathInputField.SetActive(true);
            else pathInputField.SetActive(false);
        }
        if (inputIsActive)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                string path = "assets/Blueprints/" + pathInputField.transform.Find("pathText").gameObject.GetComponent<Text>().text;
                PlanetSystem planetSystem;
                using (StreamReader streamReader = new StreamReader(path))
                {
                    string jsonString = streamReader.ReadToEnd();
                    planetSystem = JsonUtility.FromJson<PlanetSystem>(jsonString);
                }
                DestroyBlueprint();
                PlayBlueprint(planetSystem);
            }
        }
        //change to ctrl + s
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SaveBlueprint();
        }
    }

    void SaveBlueprint()
    {
        string fileName = "x";
        GameObject[] planets = GameObject.FindGameObjectsWithTag("planet");
        PlanetSystem planetSystem = new PlanetSystem(fileName, planets.Length);
        //planetSystem.planetBlueprints = new PlanetBlueprint[planets.Length];
        for (int i = 0; i < planets.Length; ++i)
        {
            Planet planetScript = planets[i].GetComponent<Planet>();
            planetSystem.planetBlueprints[i] = new PlanetBlueprint(planetScript.eccentricity,
                                                                   planetScript.semimajorAxis,
                                                                   planetScript.longitudeOfTheAscendingNode,
                                                                   planetScript.argumentOfPeriapsis,
                                                                   planetScript.trueAnomaly);
        }
        string json = JsonUtility.ToJson(planetSystem);
        string path = "assets/Blueprints/" + fileName + ".json";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        File.Create(path).Dispose();
        using (StreamWriter streamWriter = new StreamWriter(path, true))
        {
            streamWriter.Write(json);
            streamWriter.Close();
        }
    }

    void PlayBlueprint(PlanetSystem planetSystem)
    {
        ObjectManager objectManager = GetComponent<ObjectManager>();
        for (int i = 0; i < planetSystem.planetBlueprints.Length; ++i)
        {
            objectManager.AddNewObject(planetSystem.planetBlueprints[i].eccentricity,
                                       planetSystem.planetBlueprints[i].semimajorAxis,
                                       planetSystem.planetBlueprints[i].longitudeOfTheAscendingNode,
                                       planetSystem.planetBlueprints[i].argumentOfPeriapsis,
                                       planetSystem.planetBlueprints[i].trueAnomaly);
        }
    }

    public void DestroyBlueprint()
    {
        DestroyGameObjectsWithTag("planet");
        DestroyGameObjectsWithTag("arrow");
        DestroyGameObjectsWithTag("orbitLine");
    }

    private void DestroyGameObjectsWithTag(string tag)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);
        for (int i = 0; i < gameObjects.Length; ++i)
        {
            Destroy(gameObjects[i]);
        }
    }
}

[Serializable]
public class PlanetBlueprint
{
    public float eccentricity;
    public float semimajorAxis;
    public float longitudeOfTheAscendingNode;
    public float argumentOfPeriapsis;
    public float trueAnomaly;

    public PlanetBlueprint(float eccentricity,
                           float semimajorAxis,
                           float longitudeOfTheAscendingNode,
                           float argumentOfPeriapsis,
                           float trueAnomaly)
    {
        this.eccentricity = eccentricity;
        this.semimajorAxis = semimajorAxis;
        this.longitudeOfTheAscendingNode = longitudeOfTheAscendingNode;
        this.argumentOfPeriapsis = argumentOfPeriapsis;
        this.trueAnomaly = trueAnomaly;
    }
}

[Serializable]
public class PlanetSystem
{
    public string name;
    public PlanetBlueprint[] planetBlueprints;

    public PlanetSystem(string name, int planetBlueprintsCount)
    {
        this.name = name;
        planetBlueprints = new PlanetBlueprint[planetBlueprintsCount];
    }
}
