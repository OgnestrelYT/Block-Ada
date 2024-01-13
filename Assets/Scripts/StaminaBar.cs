using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaBar : MonoBehaviour
{
    public Animator animator;
    public static float staminaNow;
    public static float staminaMax = 100f;
    public float procent;

    void Start()
    {
        staminaNow = staminaMax;
    }

    void Update()
    {
        procent = staminaNow / staminaMax;
        animator.SetFloat("Stamina", procent);
    }
}
