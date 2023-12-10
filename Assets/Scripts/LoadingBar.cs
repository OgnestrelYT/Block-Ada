using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class LoadingBar : MonoBehaviour
{
    public Image bar;
    private float proc = 3000f;
    private float plus = 0f;
    public static string SceneName = "Ada room";

    void Update()
    {
        if (plus < proc)
        {
            plus += Random.Range(0f, 10f);
            if (plus >= proc)
            {
                bar.fillAmount = 1;
                SceneManager.LoadScene(SceneName);
            }
            else if (plus < proc)
            {
                bar.fillAmount = plus / proc;
            }
        }
    }
}
