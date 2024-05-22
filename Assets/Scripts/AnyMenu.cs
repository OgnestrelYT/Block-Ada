using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnyMenu : MonoBehaviour
{
    [Header("Основные настройки")]
    [SerializeField] public GameObject mainMenu; // само главное меню
    [SerializeField] public GameObject topicsParent; // парент всех топиков
    [SerializeField] public GameObject blur; // невидимый блюр, чтобы кнопки не тыкались

    [Space]
    [SerializeField] public bool isParent; // является родителем или нет
    [SerializeField] public bool hideMainMenu; // нужно ли прятать меин меню при открытии топиков


    [HideInInspector] public static bool isMainOpen; // открыт или нет главный экранчик
    [HideInInspector] public static bool isTopicOpen; // открыт ли какой-то топик
    [HideInInspector] public static bool inArea; // находится ли в зоне действия
    [HideInInspector] public static bool interactionAllow; // можно ли взаимодействовать


    void Start()
    {
        PauseMenu.canOpen = true;
        if (isParent) {
            mainMenu.SetActive(false);
        }

        GameObject[] m_Child = new GameObject[topicsParent.transform.childCount]; // скрытие всех окон топиков
        for (int i = 0; i < m_Child.Length; i++)
        {
            topicsParent.transform.GetChild(i).gameObject.SetActive(false);
        }
    }


    public void TopicButtons(GameObject window) {
        window.SetActive(true); // показ окна топика
        blur.SetActive(true); // делаем защиту от нажатий
        isTopicOpen = true;
    }


    public void OnClick() {
        if ((interactionAllow) && (inArea)) {
            if (isParent) {
                Player.canMove = false; // игрок теперь не может двигаться
                PauseMenu.canOpen = false; // нельзя открывать меню с настройками и тд
            }

            isMainOpen = true;
            mainMenu.SetActive(isMainOpen);
        }
    }


    public void BackToTheGame() {
        isMainOpen = false;
        mainMenu.SetActive(isMainOpen);

        if (isParent) {
            Player.canMove = true; // игрок теперь может двигаться
            PauseMenu.canOpen = true; // можно открывать меню с настройками и тд
        }
    }


    public void BackToTheMainMenu() {
        isTopicOpen = false;
        mainMenu.SetActive(true); // показ меин меню
        blur.SetActive(false); // убираем защиту от нажатий

        GameObject[] m_Child = new GameObject[topicsParent.transform.childCount]; // скрытие всех окон топиков
        for (int i = 0; i < m_Child.Length; i++)
        {
            topicsParent.transform.GetChild(i).gameObject.SetActive(false);
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isMainOpen) {
                if (isTopicOpen) {
                    BackToTheMainMenu();
                } else {
                    BackToTheGame();
                }
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            inArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            inArea = false;
        }
    }
}