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
    public Transform parent; // parent куда будут складыватся все tile

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
        int c = 0;

        string[] levelHelp = levelTxt.text.Split("\n", StringSplitOptions.None);
        string[][] level = new string[levelHelp.Length][];
        for (int i = 0; i < levelHelp.Length; i++)
        {
            level[i] = levelHelp[i].Split(new Char[] { ' ' });
        }

        for (int y = 2; y > 0; y--) {
            for (int x = 0; x < 2; x++) {
                Debug.Log(x * WH);
                Instantiate(tile[int.Parse(level[y][x])], new Vector3(x * WH, y * WH, 0), Quaternion.identity, parent);
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
            //Player.canMove = false * Scenes.acts[Scenes.numAct].moveAllow;
            isActivate = true;
            PauseMenu.canOpen = false;
            if (cam.m_Lens.OrthographicSize >= zoom) {
                cam.m_Lens.OrthographicSize -= Time.deltaTime * speedZoom;
            } else {
                taskMenu.SetActive(true);
            }
            
            if (Input.GetKeyDown(KeyCode.Escape)) {
                clicked = false;
            }
        } else {
            cam.Follow = player;
            PauseMenu.canOpen = true;
            taskMenu.SetActive(false);
            //Player.canMove = true * Scenes.acts[Scenes.numAct].moveAllow;
            if (cam.m_Lens.OrthographicSize <= normalZoom) {
                cam.m_Lens.OrthographicSize += Time.deltaTime * speedZoom;
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
