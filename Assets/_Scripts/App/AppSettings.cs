using System;
using System.Collections;
using System.Collections.Generic;
using Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppSettings : Singleton<AppSettings>
{
    private void Awake()
    {
        Application.targetFrameRate = Screen.currentResolution.refreshRate;
    }
}
