using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Level : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    public Animator animator;
    public bool startPos;
    public bool inArea;
    public bool IsOn;
    public int number;

    private void Start()
    {
        animator.SetBool("IsOn", startPos);
        IsOn = startPos;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Mathf.Abs(eventData.delta.x) < Mathf.Abs(eventData.delta.y))
        {
            if (inArea)
            {   
                if (eventData.delta.y > 0){
                    animator.SetBool("IsOn", true);
                    IsOn = true;
                } else {
                    animator.SetBool("IsOn", false);
                    IsOn =  false;
                }
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
        if (number == 1)
        {
            BoolMath.a = IsOn;
        } 
        else if (number == 2)
        {
            BoolMath.b = IsOn;
        }
        else if (number == 3)
        {
            BoolMath.c = IsOn;
        }
        else if (number == 4)
        {
            BoolMath.d = IsOn;
        }
        else if (number == 5)
        {
            BoolMath.e = IsOn;
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
