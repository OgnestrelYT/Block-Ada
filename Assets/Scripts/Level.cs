using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Level : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    public Animator animator;
    public bool inArea;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Mathf.Abs(eventData.delta.x) < Mathf.Abs(eventData.delta.y))
        {
            if (inArea)
            {
                if (eventData.delta.y > 0){
                    animator.SetBool("IsOn", true);
                } else {
                    animator.SetBool("IsOn", false);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            inArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            inArea = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }
}
