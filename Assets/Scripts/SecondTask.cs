using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Cinemachine;
using System;
using UnityEngine.UI;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class SecondTask : MonoBehaviour
{
    [Space]
    [Header("Аниматор:")]
    public Animator animator;

    [Space]
    [Header("ID:")]
    public TextAsset levelTxt; // ID уровня
    public string ID; // ID экранчика

    [Space]
    [Header("Разрешения/начальные константы:")]
    public static bool isTrue = false;
    public bool canUse = true;

    [Space]
    [Header("Объекты:")]
    public CinemachineVirtualCamera cam; // cinemachine камера
    public Transform taskObj; // трансформ объект задания
    public Transform player; // трансформ объект игрока
    public GameObject taskMenu; // экран(UI) задания
    public GameObject other; // другие менюшки
    public Transform parentForGM; // parent куда будут складыватся GameMap
    public InputField inputField; // поле ввода
    public GameObject car; // робот
    public GameObject helpMenu; // меню помощи

    [Space]
    [Header("Анимации:")]
    public Animator BG;

    [Space]
    [Header("Шаблон:")]
    public GameObject[] tile;
    [Space]
    public GameObject gameMap;
    public GameObject carModel;

    [Space]
    [Header("Настройки зума:")]
    public float zoom = 1.5f;
    public float normalZoom = 4.92f;
    public float speedZoom = 8f;

    [Space]
    [Header("Настройки gamemap:")]
    [SerializeField] public static int startX = 705;
    [SerializeField] public static int startY = 1035;
    [SerializeField] public static int xGamemap = 14;
    [SerializeField] public static int yGamemap = 12;
    [SerializeField] public static int WH = 90;

    [Space]
    [HideInInspector, SerializeField] public string[] codes;
    [HideInInspector, SerializeField] public bool inArea;
    [HideInInspector, SerializeField] public bool isActivate;
    [HideInInspector, SerializeField] public bool clicked = false;
    [HideInInspector, SerializeField] public string codeToSave, savedText;
    [HideInInspector, SerializeField] public GameObject gm;
    [HideInInspector, SerializeField] public static int c;


    private void Start()
    {
        Car.ID = ID;
        if (!PlayerPrefs.HasKey("isFirst")) {
            PlayerPrefs.SetInt("isFirst", 1);
        }

        if (!PlayerPrefs.HasKey("AllIDbool")) {
            PlayerPrefs.SetString("AllIDbool", ID);
        }

        if (PlayerPrefs.HasKey(ID + "bool")) {
            if (PlayerPrefs.GetInt(ID + "bool") == 1) {
                isTrue = true;
            } else {
                isTrue = false;
            }
        } else {
            PlayerPrefs.SetString("AllIDbool", PlayerPrefs.GetString("AllIDbool") + "|" + ID);
            PlayerPrefs.SetInt(ID + "bool", 0);
        }

        animator.SetBool("isTrue", isTrue);
        taskMenu.SetActive(false);
        other.SetActive(false);
        helpMenu.SetActive(false);
        codeToSave = inputField.text;

        PlayerPrefs.Save();
    }

    public void OnEnter() {
        isActivate = true;
    }

    public void OnExit() {
        isActivate = false;
    }

    public void OnClick() {
        clicked = true;
        if ((canUse) && (inArea)) {
            OnActivate();
        }
    }

    public void OnActivate() {
        if (PlayerPrefs.GetInt("isFirst") == 1) {
            helpMenu.SetActive(true);
        }

        CodeCompilating.activeScene = true;

        if (!PlayerPrefs.HasKey("AllID")) {
            PlayerPrefs.SetString("AllID", ID);
        }

        if (PlayerPrefs.HasKey(ID)) {
            Debug.Log(PlayerPrefs.GetString(ID));
            savedText = PlayerPrefs.GetString(ID);
            inputField.text = savedText;
        } else {
            PlayerPrefs.SetString("AllID", PlayerPrefs.GetString("AllID") + "|" + ID);
            savedText = codeToSave = "";
            inputField.text = savedText;
            Save();
        }

        c = 1;

        string[] levelHelp = levelTxt.text.Split("\n", StringSplitOptions.None);
        string[][] level = new string[levelHelp.Length][];
        for (int i = 0; i < levelHelp.Length; i++)
        {
            level[i] = levelHelp[i].Split(new string[] { "\t" }, StringSplitOptions.None);
        }

        gm = Instantiate(gameMap, new Vector3(0, 0, 0), Quaternion.identity, parentForGM);

        for (int y = 0; y < yGamemap; y++) {
            for (int x = 0; x < xGamemap; x++) {
                if (int.Parse(level[y][x]) == 99) {
                    c += 1;
                    Instantiate(tile[0], new Vector3(startX + (x * WH), startY - (y * WH), 0), Quaternion.identity, gm.transform);
                } else {
                    Instantiate(tile[int.Parse(level[y][x])], new Vector3(startX + (x * WH), startY - (y * WH), 0), Quaternion.identity, gm.transform);
                }
            }
        }

        car.transform.position = new Vector3(startX + ((c % xGamemap - 1) * WH), startY - ((c / xGamemap) * WH), 0);

        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetString("AllID"));
    }

    public static void StartLocation(GameObject car) {
        CodeCompilating.isObstacle = false;
        Vector3 rotate = car.transform.eulerAngles;
        rotate.z = 0;
        car.transform.rotation = Quaternion.Euler(rotate);
        car.transform.position = new Vector3(startX + ((c % xGamemap - 1) * WH), startY - ((c / xGamemap) * WH), 0);
    }

    public void Change() {
        codeToSave = inputField.text;
        CodeCompilating.Checking(codeToSave);
    }

    public void Save() {
        Debug.Log("Save");
        PlayerPrefs.SetString(ID, codeToSave);
        PlayerPrefs.Save();
    }

    public void Close() {
        PlayerPrefs.SetInt("isFirst", 0);
        helpMenu.SetActive(false);
    }

    private void Update()
    {
        if ((clicked) && (canUse) && (inArea)) {
            cam.Follow = taskObj;
            Player.canMove = false;
            isActivate = true;
            PauseMenu.canOpen = false;
            taskMenu.SetActive(true);
            BG.SetBool("isActive", true);
            if (cam.m_Lens.OrthographicSize >= zoom) {
                cam.m_Lens.OrthographicSize -= Time.deltaTime * speedZoom;
            }
            if (BG.GetCurrentAnimatorStateInfo(0).IsName("Active")) {
                other.SetActive(true);
            }
            
            if (Input.GetKeyDown(KeyCode.Escape)) {
                clicked = false;
                Player.canMove = true;
                Destroy(gm);
            }
        } else {
            CodeCompilating.activeScene = false;
            cam.Follow = player;
            PauseMenu.canOpen = true;
            other.SetActive(false);
            BG.SetBool("isActive", false);
            if (cam.m_Lens.OrthographicSize <= normalZoom) {
                cam.m_Lens.OrthographicSize += Time.deltaTime * speedZoom;
            }
            taskMenu.SetActive(false);
            clicked = false;
        }

        animator.SetBool("isTrue", isTrue);
        animator.SetBool("isActivate", isActivate);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            animator.SetBool("inArea", true);
            inArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            animator.SetBool("inArea", false);
            inArea = false;
        }
    }

    public void Firstly() {
		PlayerPrefs.SetInt("isFirst", 1);
	}

    public void DeleteAllCodes() {
		string[] splitID = PlayerPrefs.GetString("AllID").Split();
		PlayerPrefs.DeleteKey("AllID");
		foreach (string id in splitID) {
			PlayerPrefs.DeleteKey(id);
		}
	}
}
