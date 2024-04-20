using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CodeCompilating : MonoBehaviour
{
    [Space]
    [Header("Команды:")]
    public TextAsset commandsTxt; // txt с командами

    public GameObject car;

    [Space]
    [Header("Команды:")]
    public float movement;
    public float speed;

    [HideInInspector, SerializeField] public static string[][] commands;
    [HideInInspector, SerializeField] public static string[] code;
    [HideInInspector, SerializeField] public static bool isCorrect;
    [HideInInspector, SerializeField] public static int inCorrectLine;
    [HideInInspector, SerializeField] public List<string> naprList;
    [HideInInspector, SerializeField] public bool start;
    [HideInInspector, SerializeField] public int ind;
    [HideInInspector, SerializeField] public float per;

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
        if (start) {
            Debug.Log(per);
            if (per >= SecondTask.WH) {
                per = 0f;
                if (ind+1 < naprList.Count) {
                    ind += 1;
                } else {
                    ind = 0;
                    per = 0f;
                    start = false;
                }
            } else {
                if (naprList[ind] == "up") {
                    car.transform.position = Vector2.MoveTowards(car.transform.position, new Vector2(car.transform.position.x, car.transform.position.y + SecondTask.WH), speed * Time.deltaTime);
                } else if (naprList[ind] == "down") {
                    car.transform.position = Vector2.MoveTowards(car.transform.position, new Vector2(car.transform.position.x, car.transform.position.y - SecondTask.WH), speed * Time.deltaTime);
                } else if (naprList[ind] == "left") {
                    car.transform.position = Vector2.MoveTowards(car.transform.position, new Vector2(car.transform.position.x - SecondTask.WH, car.transform.position.y), speed * Time.deltaTime);
                } else if (naprList[ind] == "right") {
                    car.transform.position = Vector2.MoveTowards(car.transform.position, new Vector2(car.transform.position.x + SecondTask.WH, car.transform.position.y), speed * Time.deltaTime);
                }
                per += speed * Time.deltaTime;
            }
        }
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

    public void StopCode() {
        SecondTask.StartLocation(car);
    }

    public void StartCode() {
        ind = 0;
        per = 0f;
        naprList.Clear();
        start = true;
        SecondTask.StartLocation(car);
        if (isCorrect) {
            Debug.Log("Starting...");
            for (int i = 0; i < code.Length; i++) {
                string line = code[i].ToLower();
                line = line.Replace(" ", "");
                if (line != "") {
                    if (Array.Exists(commands[0], c => c == line)) {
                        naprList.Add("up");
                    } else if (Array.Exists(commands[1], c => c == line)) {
                        naprList.Add("down");
                    } else if (Array.Exists(commands[2], c => c == line)) {
                        naprList.Add("left");
                    } else if (Array.Exists(commands[3], c => c == line)) {
                        naprList.Add("right");
                    }
                }
            }
        } else {
            Debug.Log("Syntax error");
            Debug.Log(inCorrectLine);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Obstacle")
        {
            Debug.Log("Obstacle");
            StopCode();
        }
    }
}
