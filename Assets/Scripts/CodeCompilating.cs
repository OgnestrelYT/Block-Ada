using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CodeCompilating : MonoBehaviour
{
    [Space]
    [Header("Команды:")]
    public TextAsset commandsTxt; // txt с командами

    [HideInInspector, SerializeField] public static string[][] commands;
    [HideInInspector, SerializeField] public static string[] code;
    [HideInInspector, SerializeField] public static bool isCorrect;

    void Start()
    {
        string[] commandsHelp = commandsTxt.text.Split("\n", StringSplitOptions.None);
        commands = new string[commandsHelp.Length][];
        for (int i = 0; i < commandsHelp.Length; i++)
        {
            commands[i] = commandsHelp[i].Split(new string[] { "\t" }, StringSplitOptions.None);
        }
    }

    void Update()
    {
        
    }

    public static void Checking(string codeToCheck) {
        code = codeToCheck.Split(new string[] { "\n" }, StringSplitOptions.None);
        isCorrect = true;
        for (int i = 0; i < code.Length; i++) {
            bool found = false;
            for (int j = 0; j < commands.Length; j++)  {
                if (Array.Exists(commands[j], c => c == code[i].ToLower())) {
                    found = true;
                    break;
                }
            } 
            if (!found) {
                isCorrect = false;
                break;
            }
        }
    }

    public static void StartCode() {
        if (isCorrect) {
            Debug.Log("Starting...");
            for (int i = 0; i < code.Length; i++) {
                string line = code[i].ToLower();
                if (Array.Exists(commands[0], c => c == line)) {
                    Debug.Log("up");
                } else if (Array.Exists(commands[1], c => c == line)) {
                    Debug.Log("down");
                } else if (Array.Exists(commands[2], c => c == line)) {
                    Debug.Log("left");
                } else if (Array.Exists(commands[3], c => c == line)) {
                    Debug.Log("right");
                }
            }
        } else {
            Debug.Log("Syntax error");
        }
    }
}
