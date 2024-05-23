using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class Scenes : MonoBehaviour
{
    [HideInInspector, SerializeField] public int countActs; // всего актов
    [HideInInspector, SerializeField] public static int numAct = 0; // акт сейчас
    [HideInInspector] public static bool canSkipD; // можно ли идти дальше
    [HideInInspector] public static bool canSkipA; // можно ли идти дальше
    [HideInInspector] public static bool canSkipMenu; // можно ли идти дальше
    [HideInInspector] public static bool canSkipCheck; // можно ли идти дальше
    [HideInInspector] public static bool canSkipFinish; // можно ли идти дальше
    [HideInInspector] public static bool canSkipLever; // можно ли идти дальше
    [HideInInspector] public static bool canSkipHelpMenu; // можно ли идти дальше
    [HideInInspector] public static bool canSkipT; // можно ли идти дальше
    [HideInInspector] public static bool canSkipTime; // можно ли идти дальше
    [HideInInspector] public static float timer = 0f; // таймер
    [HideInInspector] public static string objtag = ""; // тэг для столкновений
    [HideInInspector] public static bool need = false; // нужно ли проверять столкновения

    [Header("Редактирование:")]
    [SerializeField] public string ID; // ID сценария

    [Space]    
    [Header("Редактирование:")]
	[SerializeField] private Act[] acts; // настраиваемый вручную список актов

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
        [SerializeField] public GameObject[] imageToShow; // показ картинки

        [Space]
        [SerializeField] public bool dark; // настраиваемый список диалога

        [Space]
        [SerializeField] public bool showHelp; // показывать ли окно помощи

        [Space]
        [Header("Разрешения:")]
        public bool moveAllow; // можно ли двигаться
        public bool interactionAllow; // можно ли взаимодействовать
        public bool interactionAllowDoor; // можно ли взаимодействовать с дверьми
        public bool interactionAllowPivo; // можно ли взаимодействовать с пивом
        public bool isCheckOpeningMenu; // нужно ли проверять открыл игрок вторую головоломку или нет
        public bool isCheckAny; // нужно ли проверять булеву переменную и здругих классов
        public bool needToCheckSecondTask; // нужно ли проверять что игрок прошел головоломку
        public bool needToCheckLevers; // нужно ли проверять что игрок прошел рычаги

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
        public string[] animNum;
        public bool isLoop; // надо ли зацикливать
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
        canSkipMenu = false;
        canSkipCheck = false;
        canSkipFinish = false;
        canSkipLever = false;
        canSkipHelpMenu = false;
        canSkipD = false;
        canSkipT = false;
        canSkipTime = false;
        if (!PlayerPrefs.HasKey("AllScenes")) {
            PlayerPrefs.SetString("AllScenes", ID);
        }

        if (PlayerPrefs.HasKey(ID + "Scene")) {
            numAct = PlayerPrefs.GetInt(ID + "Scene");
        } else {
            PlayerPrefs.SetString("AllScenes", PlayerPrefs.GetString("AllScenes") + "|" + ID);
            PlayerPrefs.SetInt(ID + "Scene", 0);
            numAct = 0;
        }

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

        // Проверка на открытие менюшки второй головоломки
        if (acts[numAct].isCheckOpeningMenu) {
            canSkipMenu = false;
        } else {
            canSkipMenu = true;
        }


        // Проверка тру фолз просто, которую можно менять
        if (acts[numAct].isCheckAny) {
            canSkipCheck = false;
        } else {
            canSkipCheck = true;
        }


        // Проверка прохождения второй головоломки
        if (acts[numAct].needToCheckSecondTask) {
            canSkipFinish = false;
        } else {
            canSkipFinish = true;
        }


        // Проверка прохождения первой головоломки
        if (acts[numAct].needToCheckLevers) {
            canSkipLever = false;
        } else {
            canSkipLever = true;
        }
        

        if (acts[numAct].showHelp) {
            HelpMenu.needShow = true;
            canSkipHelpMenu = false;
        } else {
            HelpMenu.needShow = false;
            canSkipHelpMenu = true;
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
            for (int i = 0; i < acts[numAct].animations.animationsList.Length; i++)
            {
                acts[numAct].animations.animationsList[i].SetBool(acts[numAct].animations.animNum[i], true);
            }
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

        // Показ картинок
        if (acts[numAct].imageToShow.Length > 0) {
            for (int i = 0; i < acts[numAct].imageToShow.Length; i += 1) {
                acts[numAct].imageToShow[i].SetActive(true);
            }
        }
    }


    public void Update()
    {
        SecondTask.interactionAllow = acts[numAct].interactionAllow;
        Lever.interactionAllow = acts[numAct].interactionAllow;
        AnyMenu.interactionAllow = acts[numAct].interactionAllowPivo;

        if (acts[numAct].isCheckOpeningMenu) {
            canSkipMenu = SecondTask.openFirstly;
        }

        if (acts[numAct].needToCheckSecondTask) {
            canSkipFinish = SecondTask.isTrue;
        }

        if (acts[numAct].needToCheckLevers) {
            canSkipLever = BoolMath.isSolved;
        }

        if (acts[numAct].showHelp) {
            canSkipHelpMenu = HelpMenu.isClosed;
        }



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
            for (int i = 0; i < acts[numAct].animations.animationsList.Length; i++)
            {
                if (!(acts[numAct].animations.animationsList[i].GetCurrentAnimatorStateInfo(0).IsName(acts[numAct].animations.animNum[i]))) {
                    canSkipA = true;
                }
                if (!acts[numAct].animations.isLoop) {
                    acts[numAct].animations.animationsList[i].SetBool(acts[numAct].animations.animNum[i], false);
                }
            }
        }

        Door.interactionAllow = acts[numAct].interactionAllowDoor;

        if ((canSkipD) && (canSkipTime) && (canSkipA) && (canSkipT) && (numAct < countActs - 1) && (canSkipMenu) && (canSkipFinish) && (canSkipLever) && (canSkipHelpMenu) && (canSkipCheck)) {
            timer = 0f;
            black.SetActive(false);
            canSkipD = false;
            canSkipA = false;
            canSkipT = false;
            canSkipTime = false;
            canSkipMenu = false;
            canSkipFinish = false;
            canSkipLever = false;
            canSkipHelpMenu = false;
            canSkipCheck = false;
            for (int i = 0; i < acts[numAct].imageToShow.Length; i += 1) {
                acts[numAct].imageToShow[i].SetActive(false);
            }
            numAct++;
            Load();
        }
    }

    public void ClearAllScenes() {
        string[] splitID = PlayerPrefs.GetString("AllScenes").Split();
		PlayerPrefs.DeleteKey("AllScenes");
		foreach (string id in splitID) {
			PlayerPrefs.DeleteKey(id + "Scene");
		}
		SecondTask.isTrue = false;
    }
}
