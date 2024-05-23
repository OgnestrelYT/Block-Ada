using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoolMath : MonoBehaviour
{
    public int ID;
    public static bool x;
    public static bool y;
    public static bool z;
    public static bool isSolved;
    public bool f = true;

    void Start() 
    {
        PlayerPrefs.SetInt(ID + "lever", 0);
    }

    void Update()
    {
        if ((!z && x || x && y) == f)
        {
            if (PlayerPrefs.GetInt(ID + "lever") == 0) {
                AchievementSystem.use.AdjustAchievement(4, 1);
                AchievementSystem.use.Save();
                PlayerPrefs.SetInt(ID + "lever", 1);
            }
            isSolved = true;
        }
        else {
            isSolved = false;
        }
    }
}
