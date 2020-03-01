using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timeManager : MonoBehaviour
{
    [SerializeField] Text utc;
    [SerializeField] Text julian;
    [SerializeField] Text tdb;

    void Update()
    {
        
        utc.text = System.DateTime.UtcNow.ToString();
        julian.text = calcJulianDate(System.DateTime.UtcNow).ToString();
        tdb.text = ((calcJulianDate(System.DateTime.UtcNow) - 2451545.0f) / 365250).ToString();
    }

    private double calcJulianDate(System.DateTime date)
    {
        return date.ToOADate() + 2415018.5;
    }
}
