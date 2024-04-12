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
    public bool isTrue = false;
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

    [Space]
    [Header("Анимации:")]
    public Animator BG;

    [Space]
    [Header("Шаблон:")]
    public GameObject[] tile;
    [Space]
    public GameObject gameMap;

    [Space]
    [Header("Настройки зума:")]
    public float zoom = 1.5f;
    public float normalZoom = 4.92f;
    public float speedZoom = 8f;

    [Space]
    [Header("Настройки gamemap:")]
    [SerializeField] public int startX;
    [SerializeField] public int startY;
    [SerializeField] public int WH;

    [Space]
    [HideInInspector, SerializeField] public string[] codes;
    [HideInInspector, SerializeField] public bool inArea;
    [HideInInspector, SerializeField] public bool isActivate;
    [HideInInspector, SerializeField] public bool clicked = false;
    [HideInInspector, SerializeField] public string codeToSave, savedText;
    [HideInInspector, SerializeField] public GameObject gm;


    private void Start()
    {
        animator.SetBool("isTrue", isTrue);
        taskMenu.SetActive(false);
        other.SetActive(false);
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

        int c = 0;

        string[] levelHelp = levelTxt.text.Split("\n", StringSplitOptions.None);
        string[][] level = new string[levelHelp.Length][];
        for (int i = 0; i < levelHelp.Length; i++)
        {
            level[i] = levelHelp[i].Split(new string[] { "\t" }, StringSplitOptions.None);
        }

        gm = Instantiate(gameMap, new Vector3(0, 0, 0), Quaternion.identity, parentForGM);

        for (int y = 0; y < 12; y++) {
            for (int x = 0; x < 14; x++) {
                Instantiate(tile[int.Parse(level[y][x])], new Vector3(startX + (x * WH), startY - (y * WH), 0), Quaternion.identity, gm.transform);
                c += 1;
            }
        }

        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetString("AllID"));
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
            cam.Follow = player;
            PauseMenu.canOpen = true;
            other.SetActive(false);
            BG.SetBool("isActive", false);
            if (cam.m_Lens.OrthographicSize <= normalZoom) {
                cam.m_Lens.OrthographicSize += Time.deltaTime * speedZoom;
            }
            if (BG.GetCurrentAnimatorStateInfo(0).IsName("Inactive")) {
                taskMenu.SetActive(false);
            }
            clicked = false;
        }

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
}
