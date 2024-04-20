using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CodeCompilating : MonoBehaviour
{
    [Space]
    [Header("Команды:")]
    public TextAsset commandsTxt; // txt с командами

    [Space]
    [Header("Команды:")]
    public float movement;
    public float speed;

    [HideInInspector, SerializeField] public static string[][] commands;
    [HideInInspector, SerializeField] public static string[] code;
    [HideInInspector, SerializeField] public static bool isCorrect;
    [HideInInspector, SerializeField] public static int inCorrectLine;

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
                if (Array.Exists(commands[j], c => c == code[i].ToLower().Replace(" ", ""))) {
                    found = true;
                    break;
                }
            } 
            if (!found) {
                inCorrectLine = i+1;
                isCorrect = false;
                break;
            }
        }
    }

    public void Up() {
        for (float i = 0f; i < SecondTask.WH; i += movement) {
            SecondTask.car.transform.position += new Vector3(0, movement, 0) * speed;
        }
    }

    public void Down() {
        for (float i = 0f; i < SecondTask.WH; i += movement) {
            SecondTask.car.transform.position -= new Vector3(0, movement, 0) * speed;
        }
    }

    public void Left() {
        for (float i = 0f; i < SecondTask.WH; i += movement) {
            SecondTask.car.transform.position += new Vector3(movement, 0, 0) * speed;
        }
    }

    public void Right() {
        for (float i = 0f; i < SecondTask.WH; i += movement) {
            SecondTask.car.transform.position -= new Vector3(movement, 0, 0) * speed;
        }
    }

    public void StartCode() {
        SecondTask.StartLocation();
        if (isCorrect) {
            Debug.Log("Starting...");
            for (int i = 0; i < code.Length; i++) {
                string line = code[i].ToLower();
                line = line.Replace(" ", "");
                if (line != "") {
                    if (Array.Exists(commands[0], c => c == line)) {
                        Up();
                    } else if (Array.Exists(commands[1], c => c == line)) {
                        Down();
                    } else if (Array.Exists(commands[2], c => c == line)) {
                        Left();
                    } else if (Array.Exists(commands[3], c => c == line)) {
                        Right();
                    }
                }
            }
        } else {
            Debug.Log("Syntax error");
            Debug.Log(inCorrectLine);
        }
    }
}
