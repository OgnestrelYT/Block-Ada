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
    [HideInInspector] public static bool canSkipTime; // можно ли идти дальше
    [HideInInspector] public static float timer = 0f; // таймер
    [HideInInspector] public static string objtag = ""; // тэг для столкновений
    [HideInInspector] public static bool need = false; // нужно ли проверять столкновения


    [Header("Редактирование:")]
	[SerializeField] private Act[] acts; // настраиваемый вручную список ачивок

    [Space]

	[Header("Шаблоны:")]
	[SerializeField] private DialogueWindow dialogueWindow; // шаблон для диалогового окна
    [SerializeField] private GameObject black; // шаблон затемнения


    [System.Serializable] struct Act
	{
        [Space]
        [Header("Списки:")]
        [Space]
        [SerializeField] public float times; // время
        [Space]
        [SerializeField] public Dialogue dialogues; // настраиваемый список диалога
        [Space]
        [SerializeField] public Animation animations; // настраиваемый список анимаций
        [Space]
        [SerializeField] public GameObject[] task; // настраиваемый Task
        [Space]
        [SerializeField] public bool dark; // настраиваемый список диалога

        [Space]

        [Header("Разрешения:")]
        public bool moveAllow; // можно ли двигаться
        public bool interactionAllow; // можно ли взаимодействовать

        [Space]

        [Header("Ачивки:")]
        [SerializeField] public Achievements achievements; // настраиваемый список диалога

        [Space]

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

    [System.Serializable] struct Achievements
    {
        public int[] id;
        public int[] value;
    }


    void Start()
	{
        dialogueWindow.gameObject.SetActive(false);
        black.SetActive(false);
        countActs = acts.Length;
        canSkipA = false;
        canSkipD = false;
        canSkipT = false;
        canSkipTime = false;
        Load();
    }

    public void Load()
    {   
        // Передвижение
        Player.canMove = acts[numAct].moveAllow;

        // Ачивки
        if (acts[numAct].achievements.id.Length > 0) {
            for (int i = 0; i < acts[numAct].achievements.id.Length; i++)
            {
                AchievementSystem.use.AdjustAchievement(acts[numAct].achievements.id[i], acts[numAct].achievements.value[i]);
                AchievementSystem.use.Save();
            }
        }

        // Затемнение
        black.SetActive(acts[numAct].dark);

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
        if (acts[numAct].task.Length > 0) {
            canSkipT = false;
            need = true;
            objtag = acts[numAct].task[0].tag;
        } else {
            need = false;
            canSkipT = true;
        }
    }


    public void Update()
    {
        // Время
        if (acts[numAct].times > 0f) {
            timer += Time.deltaTime;
            if (timer > acts[numAct].times) {
                canSkipTime = true;
            }
        } else {
            canSkipTime = true;
        }

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

        if ((canSkipD) && (canSkipTime) && (canSkipA) && (canSkipT) && (numAct < countActs - 1)) {
            timer = 0f;
            black.SetActive(false);
            canSkipD = false;
            canSkipA = false;
            canSkipT = false;
            canSkipTime = false;
            numAct++;
            Load();
        }
    }
}
