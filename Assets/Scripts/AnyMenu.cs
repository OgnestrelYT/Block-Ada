using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnyMenu : MonoBehaviour
{
    [Header("Основные настройки")]
    [SerializeField] public GameObject mainMenu; // само главное меню

    [Space]
    [SerializeField] private Topic[] topics; // все топики

    [HideInInspector] public static bool isMainOpen = false; // открыт или нет главный экранчик
    [HideInInspector] public static bool inArea; // находится ли в зоне действия
    [HideInInspector] public static bool interactionAllow; // можно ли взаимодействовать
    [HideInInspector] public static bool topicOpened; // открыт ли какой-то топик


    [System.Serializable] struct Topic
	{
        [Space]
        [SerializeField] public string name; // название топика
        [SerializeField] public Button buttonForOpen; // кнопка для открытия топика
        [SerializeField] public GameObject openedTopic; // открытый топик

        [HideInInspector] public bool isTopicOpen; // открыт или нет топик
    }


    void Start()
    {
        mainMenu.SetActive(isMainOpen);
        for (int i = 0; i < topics.Length; i++) {
            topics[i].openedTopic.SetActive(topics[i].isTopicOpen);
        }
    }


    public void TopicButtons(int n) {
        topics[n].isTopicOpen = true;
        topicOpened = true;
    }


    public void OnClick() {
        if ((interactionAllow) && (inArea)) {
            PauseMenu.canOpen = false;
            isMainOpen = true;
            mainMenu.SetActive(isMainOpen);
        }
    }


    public void BackToTheGame() {
        isMainOpen = false;
        mainMenu.SetActive(isMainOpen);
        PauseMenu.canOpen = true;
    }


    public void BackToTheMainMenu() {
        isMainOpen = true;
        topicOpened = false;
        for (int i = 0; i < topics.Length; i++) {
            topics[i].isTopicOpen = false;
        }
    }


    void Update()
    {
        for (int i = 0; i < topics.Length; i++) {
            Debug.Log(topics[i].isTopicOpen);
            topics[i].openedTopic.SetActive(topics[i].isTopicOpen);
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isMainOpen) {
                BackToTheGame();
            } else if (topicOpened) {
                BackToTheMainMenu();
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