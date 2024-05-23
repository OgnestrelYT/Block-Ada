using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Lever : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    public Animator animator;
    public bool startPos;
    public bool inArea;
    public bool IsOn;
    public int number;
    public static bool interactionAllow;

    private void Start()
    {
        animator.SetBool("IsOn", startPos);
        IsOn = startPos;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if ((Mathf.Abs(eventData.delta.x) < Mathf.Abs(eventData.delta.y)) && (PlayerPrefs.GetInt("LeverDrag") == 0))
        {
            if ((inArea) && (interactionAllow))
            {   
                if (eventData.delta.y > 0){
                    IsOn = true;
                    animator.SetBool("IsOn", IsOn);
                } else {
                    IsOn =  false;
                    animator.SetBool("IsOn", IsOn);
                }
            }
        }
    }

    public void onClick() {
        if ((PlayerPrefs.GetInt("LeverDrag") == 1) && (interactionAllow)) {
            if (IsOn) {
                IsOn = false;
                animator.SetBool("IsOn", IsOn);
            } else {
                IsOn = true;
                animator.SetBool("IsOn", IsOn);
            }
        }
    }

    public void OnEnter() {
        animator.SetBool("isAct", true);
    }

    public void OnExit() {
        animator.SetBool("isAct", false);
    }

    private void Update()
    {
        Debug.Log(IsOn);
        if (number == 1)
        {
            BoolMath.x = IsOn;
        } 
        else if (number == 2)
        {
            BoolMath.y = IsOn;
        }
        else if (number == 3)
        {
            BoolMath.z = IsOn;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            animator.SetBool("InArea", true);
            inArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            animator.SetBool("InArea", false);
            inArea = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }

}
