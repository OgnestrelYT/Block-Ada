using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Cinemachine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class SecondTask : MonoBehaviour
{
    public Animator animator;
    public bool isTrue;
    public CinemachineVirtualCamera cam;
    public Transform taskObj;
    public Transform player;
    public float zoom = 1.5f;
    public float normalZoom = 4.92f;
    public float speedZoom = 3f;
    [HideInInspector, SerializeField] public bool inArea;
    [HideInInspector, SerializeField] public bool isActivate;
    [HideInInspector, SerializeField] public bool clicked = false;

    private void Start()
    {
        animator.SetBool("isTrue", isTrue);
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
        if (clicked) {
            cam.Follow = taskObj;
            //Player.canMove = false * Scenes.acts[Scenes.numAct].moveAllow;
            isActivate = true;
            if (cam.m_Lens.OrthographicSize >= zoom) {
                cam.m_Lens.OrthographicSize -= Time.deltaTime * speedZoom;
            }
        } else {
            cam.Follow = player;
            //Player.canMove = true * Scenes.acts[Scenes.numAct].moveAllow;
            if (cam.m_Lens.OrthographicSize <= normalZoom) {
                cam.m_Lens.OrthographicSize += Time.deltaTime * speedZoom;
            }
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
