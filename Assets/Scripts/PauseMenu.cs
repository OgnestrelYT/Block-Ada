using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class PauseMenu : MonoBehaviour
{
    public bool PauseGame = false;
    public bool InMain = false;
    public bool InSettings = false;
    public bool InAchievements = false;
    public GameObject pauseGameMenu;
    public GameObject pauseMain;
    public GameObject pauseSettings;
    public GameObject pauseAchievements;
    

    void Start(){
        pauseGameMenu.SetActive(false);
        pauseMain.SetActive(false);
    }

    void Update()
    {
        Player.isStop = PauseGame;
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (PauseGame){
                if (InMain){
                    Resume();
                } else {
                    PlayerPrefs.Save();
                    Back();
                }

            } else {
                Pause();
            }
        }


    }

    public void Resume(){
        pauseGameMenu.SetActive(false);
        Time.timeScale = 1f;
        PauseGame = false;
    }

    public void Pause(){
        InMain = true;
        pauseGameMenu.SetActive(true);
        pauseMain.SetActive(true);
        pauseSettings.SetActive(false);
        pauseAchievements.SetActive(false);
        Time.timeScale = 0f;
        PauseGame = true;
    }

    public void Settings(){
        InMain = false;
        InSettings = true;
        pauseMain.SetActive(false);
        pauseSettings.SetActive(true);
        PauseGame = true;
    }

    public void Achievements(){
        InMain = false;
        InAchievements = true;
        pauseMain.SetActive(false);
        AchievementSystem.use.ShowAchievementList(true);
        pauseAchievements.SetActive(true);
        PauseGame = true;
    }

    public void Exit(){
        PlayerPrefs.Save();
        LoadingBar.SceneName = "Main menu";
        Time.timeScale = 1f;
        SceneManager.LoadScene("Loading Scene");
    }

    public void Back(){
        InMain = true;
        pauseAchievements.SetActive(false);
        if (AchievementSystem.isActive) {
            AchievementSystem.use.ShowAchievementList(false);
        }
        pauseSettings.SetActive(false);
        pauseMain.SetActive(true);
        pauseGameMenu.SetActive(true);
    }
}
