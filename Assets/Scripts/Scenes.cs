using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class Scenes : MonoBehaviour
{
    [HideInInspector, SerializeField] public int countActs; // всего актов
    [HideInInspector, SerializeField] public int numAct = 0; // акт сейчас
    [HideInInspector] public static bool canSkipD; // можно ли идти дальше
    [HideInInspector] public static bool canSkipA; // можно ли идти дальше
    [HideInInspector] public static bool canSkipT; // можно ли идти дальше


    [Header("Редактирование:")]
	[SerializeField] private Act[] acts; // настраиваемый вручную список ачивок

    [Space]

	[Header("Шаблоны:")]
	[SerializeField] private DialogueWindow dialogueWindow; // шаблон для диалогового окна


    [System.Serializable] struct Act
	{
        [Space]
        [Header("Списки:")]
        [Space]
        [SerializeField] public Dialogue dialogues; // настраиваемый список диалога
        [Space]
        [SerializeField] public Animation animations; // настраиваемый список анимаций
        [Space]
        [SerializeField] public Tasks tasks; // настраиваемый список диалога

        [Space]

        [Header("Разрешения:")]
        public bool moveAllow; // можно ли двигаться
        public bool interactionAllow; // можно ли взаимодействовать

        [Header("Описание:")]
        public string description; // описание чисто для кодера

        [HideInInspector, SerializeField] public bool isCompleted; // выполнено или нет
        
	}

    [System.Serializable] struct Dialogue
    {
        public string[] messages;
        public Sprite avatar;
		public Sprite bg;
		public string name;
    }

    [System.Serializable] struct Animation
    {
        public Animator[] animationsList; // список анимаций
    }

    [System.Serializable] struct Tasks
    {
        public GameObject[] gameObjList; // список заданий
    }


    void Awake()
	{
        dialogueWindow.gameObject.SetActive(false);
        countActs = acts.Length;
        canSkipA = false;
        canSkipD = false;
        canSkipT = false;
        Load();
    }

    public void Load()
    {   
        Player.canMove = acts[numAct].moveAllow;
        Debug.Log(acts[numAct].animations.animationsList.Length);

        // Диалоги
        if (acts[numAct].dialogues.messages.Length > 0) {
            dialogueWindow.SetDialogue(acts[numAct].dialogues.avatar, acts[numAct].dialogues.bg, acts[numAct].dialogues.name, acts[numAct].dialogues.messages);
        } else {
            canSkipD = true;
        }
        
        // Анимации
        if (acts[numAct].animations.animationsList.Length > 0) {
            Debug.Log("Iphone");
            for (int i = 0; i < acts[numAct].animations.animationsList.Length; i++)
            {
                acts[numAct].animations.animationsList[i].SetBool("isStart", true);
            }
            Debug.Log("asdasdasdasdasdasdasd");
        } else {
            canSkipA = true;
        }

        // Задания
        if (acts[numAct].tasks.gameObjList.Length > 0) {
            Debug.Log("Iasd");
        } else {
            canSkipT = true;
        }
    }


    public void Update()
    {
        if (acts[numAct].animations.animationsList.Length > 0) {
            Debug.Log("Iphone");
            for (int i = 0; i < acts[numAct].animations.animationsList.Length; i++)
            {
                if (!(acts[numAct].animations.animationsList[i].GetCurrentAnimatorStateInfo(0).IsName("Using"))) {
                    canSkipA = true;
                }
                acts[numAct].animations.animationsList[i].SetBool("isStart", false);
            }
        }

        if ((canSkipD) && (canSkipA) && (canSkipT) && (numAct < countActs - 1)) {
            canSkipD = false;
            canSkipA = false;
            canSkipT = false;
            numAct++;
            Load();
        }
    }
}
