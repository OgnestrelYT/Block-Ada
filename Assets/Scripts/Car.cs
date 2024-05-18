using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public GameObject car;
    public Animator animator;
    public static string ID;

    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("isActive", SecondTask.isTrue);
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isActive", SecondTask.isTrue);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Obstacle") {
            CodeCompilating.isObstacle = true;
            CodeCompilating.start = false;
        } else if (other.tag == "Finish") {
            if (PlayerPrefs.GetInt(ID + "bool") == 0) {
                AchievementSystem.use.AdjustAchievement(1, 1);
                PlayerPrefs.SetInt(ID + "bool", 1);
            }
            
            CodeCompilating.finished = true;
            animator.SetBool("isActive", true);
            AchievementSystem.use.Save();
        }
    }
}
