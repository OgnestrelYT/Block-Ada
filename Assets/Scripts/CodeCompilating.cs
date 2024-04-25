using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CodeCompilating : MonoBehaviour
{
    [Space]
    [Header("Команды:")]
    public TextAsset commandsTxt; // txt с командами

    [Space]
    [Header("Объекты:")]
    public GameObject car;
    public GameObject cor;
    public GameObject inCor;
    public Text errorText;

    [Space]
    [Header("Скорости:")]
    public float movement;
    public float speed;
    public float rotationSpeed;

    [HideInInspector, SerializeField] public static string[][] commands;
    [HideInInspector, SerializeField] public static string[] code;
    [HideInInspector, SerializeField] public static bool isCorrect;
    [HideInInspector, SerializeField] public static int inCorrectLine;
    [HideInInspector, SerializeField] public List<string> naprList;
    [HideInInspector, SerializeField] public static bool start;
    [HideInInspector, SerializeField] public static bool isObstacle = false;
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
        errorText.text = "";
    }

    void Update()
    {
        if (isCorrect) {
            if (start) {
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
                        if ((float)Math.Round(car.transform.rotation.eulerAngles.z) == 90f) {
                            car.transform.position = Vector2.MoveTowards(car.transform.position, new Vector2(car.transform.position.x, car.transform.position.y + SecondTask.WH), speed * Time.deltaTime);
                            per += speed * Time.deltaTime;
                        } else {
                            if (((float)Math.Abs(90f - car.transform.rotation.eulerAngles.z) > 180f) || ((float)(90f - car.transform.rotation.eulerAngles.z) < 0f))  {
                                car.transform.Rotate(0, 0, Time.deltaTime * -rotationSpeed);
                            } else {
                                car.transform.Rotate(0, 0, Time.deltaTime * rotationSpeed);
                            }
                        }
                    } else if (naprList[ind] == "down") {
                        if ((float)Math.Round(car.transform.rotation.eulerAngles.z) == 270f) {
                            car.transform.position = Vector2.MoveTowards(car.transform.position, new Vector2(car.transform.position.x, car.transform.position.y - SecondTask.WH), speed * Time.deltaTime);
                            per += speed * Time.deltaTime;
                        } else {
                            if (((float)Math.Abs(270f - car.transform.rotation.eulerAngles.z) > 180f) || ((float)(270f - car.transform.rotation.eulerAngles.z) < 0f))  {
                                car.transform.Rotate(0, 0, Time.deltaTime * -rotationSpeed);
                            } else {
                                car.transform.Rotate(0, 0, Time.deltaTime * rotationSpeed);
                            }
                        }
                    } else if (naprList[ind] == "left") {
                        if ((float)Math.Round(car.transform.rotation.eulerAngles.z) == 180f) {
                            car.transform.position = Vector2.MoveTowards(car.transform.position, new Vector2(car.transform.position.x - SecondTask.WH, car.transform.position.y), speed * Time.deltaTime);
                            per += speed * Time.deltaTime;
                        } else {
                            if ((float)(180f - car.transform.rotation.eulerAngles.z) < 0f)  {
                                car.transform.Rotate(0, 0, Time.deltaTime * -rotationSpeed);
                            } else {
                                car.transform.Rotate(0, 0, Time.deltaTime * rotationSpeed);
                            }
                        }
                    } else if (naprList[ind] == "right") {
                        if ((float)Math.Round(car.transform.rotation.eulerAngles.z) == 0f) {
                            car.transform.position = Vector2.MoveTowards(car.transform.position, new Vector2(car.transform.position.x + SecondTask.WH, car.transform.position.y), speed * Time.deltaTime);
                            per += speed * Time.deltaTime;
                        } else {
                            if ((float)Math.Abs(0f - car.transform.rotation.eulerAngles.z) > 180f)  {
                                car.transform.Rotate(0, 0, Time.deltaTime * rotationSpeed);
                            } else {
                                car.transform.Rotate(0, 0, Time.deltaTime * -rotationSpeed);
                            }
                        }
                    }
                }
            }

            if (isObstacle) {
                cor.SetActive(false); // Incorrect
                inCor.SetActive(true);
                errorText.text = "Obstacle";
            } else {
                cor.SetActive(true); // Correct
                inCor.SetActive(false);
                errorText.text = "";
            }
        } else {
            start = false;
            cor.SetActive(false); // Incorrect
            inCor.SetActive(true);
            errorText.text = "Syntax error: " + inCorrectLine.ToString();
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
        start = false;
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
}
