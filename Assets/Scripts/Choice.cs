using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Choice : MonoBehaviour
{
    [SerializeField] public Button buttonBack; // кнопка назад
    [SerializeField] public Button buttonSubmit; // кнопка согласия
    [SerializeField] public GameObject window; // окошко
    [SerializeField] public GameObject mainMenu; // окошко главного меню
    [SerializeField] public int ID; // ID работы
    
    
    void Start()
    {
        buttonSubmit.onClick.AddListener(Submit);
        buttonBack.onClick.AddListener(Close);
    }


    public void Submit() {
        PlayerPrefs.SetInt("work", ID);
        mainMenu.SetActive(false);
        Player.canMove = true; // игрок теперь может двигаться
        PauseMenu.canOpen = true; // можно открывать меню с настройками и тд
        Scenes.canSkipCheck = true;
    }


    public void Close() {
        window.SetActive(false);
    }
}
