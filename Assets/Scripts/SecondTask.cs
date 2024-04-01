using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Cinemachine;
using System;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class SecondTask : MonoBehaviour
{
    [Space]
    [Header("Аниматор:")]
    public Animator animator;

    [Space]
    [Header("Уровень:")]
    public TextAsset levelTxt;

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
    public Transform parent; // parent куда будут складыватся все tile

    [Space]
    [Header("Анимации:")]
    public Animator BG;

    [Space]
    [Header("Шаблон:")]
    public GameObject[] tile;

    [Space]
    [Header("Настройки зума:")]
    public float zoom = 1.5f;
    public float normalZoom = 4.92f;
    public float speedZoom = 8f;

    [Space]
    [HideInInspector, SerializeField] public bool inArea;
    [HideInInspector, SerializeField] public bool isActivate;
    [HideInInspector, SerializeField] public bool clicked = false;

    [SerializeField] public int startX;
    [SerializeField] public int startY;
    [SerializeField] public int WH;



    private void Start()
    {
        animator.SetBool("isTrue", isTrue);
        taskMenu.SetActive(false);
        other.SetActive(false);
        int c = 0;

        string[] levelHelp = levelTxt.text.Split("\n", StringSplitOptions.None);
        string[][] level = new string[levelHelp.Length][];
        for (int i = 0; i < levelHelp.Length; i++)
        {
            level[i] = levelHelp[i].Split(new string[] { "\t" }, StringSplitOptions.None);
        }

        for (int y = 0; y < 3; y++) {
            for (int x = 0; x < 14; x++) {
                Instantiate(tile[int.Parse(level[y][x])], new Vector3(startX + (x * WH), startY - (y * WH), 0), Quaternion.identity, parent);
                c += 1;
            }
        }
    }

    public void OnEnter() {
        isActivate = true;
    }
    public void OnExit() {
        isActivate = false;
    }

    public void OnClick() {
        clicked = true;
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
