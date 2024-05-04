using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public GameObject car;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Obstacle") {
            CodeCompilating.isObstacle = true;
            CodeCompilating.start = false;
        } else if (other.tag == "Finish") {
            CodeCompilating.finished = true;
            animator.SetBool("isActive", true);
        }
    }
}
