using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeUIView : MonoBehaviour
{
    Color DarkThemeColor = new Color(0.1126735f, 0.1126735f, 0.1126735f);
    Color LightThemeColor = new Color(1f, 1f, 1f);
    bool isDarkTheme = true;
    Camera cam;
    private void Start()
    {
        cam = GetComponent<Camera>();
    }
    public void ChangeTheme()
    {
        isDarkTheme = !isDarkTheme;
        if (isDarkTheme)
        {
            cam.backgroundColor = DarkThemeColor;
        }
        else
        {
            cam.backgroundColor = LightThemeColor;
        }
    }
}
