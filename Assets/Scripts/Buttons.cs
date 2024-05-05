using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class Buttons : MonoBehaviour
{
    public Button startButton;
    public Button creditsButton;
    public Button exitButton;
    public Button yesButton;
    public Button noButton;
    public GameObject areYouSure;
    public string SceneStart = "Ada room";
    public string SceneCredits = "Credits Scene";


    void Start() {
        startButton.onClick.AddListener(StartB);
        creditsButton.onClick.AddListener(CreditsB);
        exitButton.onClick.AddListener(ExitB);
        yesButton.onClick.AddListener(YesB);
        noButton.onClick.AddListener(NoB);
        areYouSure.SetActive(false);
    }

    public void StartB()
    {
        SceneManager.LoadScene(SceneStart);
    }

    public void CreditsB()
    {
        SceneManager.LoadScene(SceneCredits);
    }

    public void ExitB()
    {
        areYouSure.SetActive(true);
    }

    public void YesB()
    {
        areYouSure.SetActive(false);
        Application.Quit();
    }

    public void NoB()
    {
        areYouSure.SetActive(false);
    }
}
