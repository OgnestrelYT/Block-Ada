using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class Scenes : MonoBehaviour
{
    [HideInInspector, SerializeField] public int countActs; // всего актов
    [HideInInspector, SerializeField] public int numAct = 0; // акт сейчас
    [HideInInspector] public static bool canSkip; // можно ли идти дальше


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
        public bool isInteraction; // можно ли взаимодействовать
    }

    [System.Serializable] struct Tasks
    {
        public bool isInteraction; // можно ли взаимодействовать
    }


    void Awake()
	{
        dialogueWindow.gameObject.SetActive(false);
        countActs = acts.Length;
        canSkip = false;
        Load();
    }

    public void Load()
    {   
        if (acts[numAct].dialogues.messages.Length > 0) {
            dialogueWindow.SetDialogue(acts[numAct].dialogues.avatar, acts[numAct].dialogues.bg, acts[numAct].dialogues.name, acts[numAct].dialogues.messages);
        }
    }


    public void Update()
    {
        if ((canSkip) && (numAct < countActs - 1)) {
            canSkip = false;
            numAct++;
            Load();
        }
    }
}
