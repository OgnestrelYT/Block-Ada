using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [HideInInspector, SerializeField] public static bool PauseGame = false;
    [HideInInspector, SerializeField] public bool InMain = false;
    [HideInInspector, SerializeField] public static bool canOpen;
    public GameObject pauseGameMenu;
    public GameObject pauseMain;
    public GameObject pauseSettings;
    public GameObject pauseAchievements;

    [Header("Secret:")]
    public GameObject secretButtonObj;
    public GameObject pauseSecret;

    

    void Start(){
        pauseGameMenu.SetActive(false);
        pauseMain.SetActive(false);

        secretButtonObj.SetActive(false);
        pauseSecret.SetActive(false);
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
            } else if (canOpen) {
                Pause();
            }
        }

        if (InMain) {
            if (Input.GetKey(KeyCode.Slash) && Input.GetKey(KeyCode.O)) {
                secretButtonObj.SetActive(true);
            } else {
                secretButtonObj.SetActive(false);
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

        secretButtonObj.SetActive(false);
        pauseSecret.SetActive(false);

        Time.timeScale = 0f;
        PauseGame = true;
    }

    public void Settings(){
        Time.timeScale = 1f;
        InMain = false;
        pauseMain.SetActive(false);
        pauseSettings.SetActive(true);
        PauseGame = true;
    }

    public void Achievements(){
        Time.timeScale = 1f;
        InMain = false;
        pauseMain.SetActive(false);
        AchievementSystem.use.ShowAchievementList(true);
        pauseAchievements.SetActive(true);
        PauseGame = true;
    }

    // Вспомогательные кнопки
    public void Secret(){
        Time.timeScale = 1f;
        InMain = false;
        pauseMain.SetActive(false);
        pauseSecret.SetActive(true);
        PauseGame = true;
    }

    public void Exit(){
        PlayerPrefs.Save();
        LoadingBar.SceneName = "Main menu";
        Time.timeScale = 1f;
        SceneManager.LoadScene("Loading Scene");
    }

    public void Back(){
        Time.timeScale = 0f;
        InMain = true;
        pauseAchievements.SetActive(false);
        if (AchievementSystem.isActive) {
            AchievementSystem.use.ShowAchievementList(false);
        }
        pauseSecret.SetActive(false);
        pauseSettings.SetActive(false);
        pauseMain.SetActive(true);
        pauseGameMenu.SetActive(true);
    }
}
