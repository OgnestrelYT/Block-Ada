using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    public static float fadeSpeed = 1f;
    public static float time = 1f;
    public static bool smoothStart;
    public static bool smoothEnd;
    public static float t;

    IEnumerator Start()
    {
        t = 0;

        Image fadeImage = GetComponent<Image>();
        Color color = fadeImage.color;

        if (smoothStart) {
            while (color.a < 1f) {
                color.a += fadeSpeed * Time.deltaTime;
                fadeImage.color = color;
                yield return null;
            }
        }
        
        color.a = 1f;
        fadeImage.color = color;

        while (t < time) {
            t += 1f * Time.deltaTime;
            yield return null;
        }

        if (smoothEnd) {
            while (color.a > 0f) {
                color.a -= fadeSpeed * Time.deltaTime;
                fadeImage.color = color;
                yield return null;
            }
        }

        color.a = 0f;
        fadeImage.color = color;
    }
}
