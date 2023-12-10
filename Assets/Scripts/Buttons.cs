using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Buttons : MonoBehaviour
{
    public string SceneStart = "Ada room";
    public string SceneSettings = "Settings Scene";
    public string SCeneCredits = "Credits Scene";

    public void StartB()
    {
        LoadingBar.SceneName = SceneStart;
        SceneManager.LoadScene("Loading Scene");
    }

    public void SettingsB()
    {
        LoadingBar.SceneName = SceneSettings;
        SceneManager.LoadScene("Loading Scene");
    }

    public void CreditsB()
    {
        LoadingBar.SceneName = SCeneCredits;
        SceneManager.LoadScene("Loading Scene");
    }

    public void ExitB()
    {
        Application.Quit();
    }
}
