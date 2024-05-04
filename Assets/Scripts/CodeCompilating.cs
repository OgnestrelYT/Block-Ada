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
    public InputField inputField; // поле ввода

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
    [HideInInspector, SerializeField] public int n;
    [HideInInspector, SerializeField] public float per;
    [HideInInspector, SerializeField] public static bool activeScene;
    [HideInInspector, SerializeField] public static bool finished = false;

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
        if (finished) {
            errorText.text = "Finished!!!";
            SecondTask.isTrue = true;
        }


        if (!activeScene) {
            start = false;
            ind = 0;
            naprList.Clear();
            SecondTask.StartLocation(car);
        }


        if (isCorrect) {
            if (start) {
                inputField.interactable = false;
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
            } else {
                inputField.interactable = true;
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
            string[] com = code[i].Split(" ");
            if (com.Length != 0) {
                if (com.Length == 2) {
                    if (!int.TryParse(com[1], out int asd)) {
                        inCorrectLine = i+1;
                        isCorrect = false;
                        break;
                    }
                } else if ((com.Length > 2)) {
                    inCorrectLine = i+1;
                    isCorrect = false;
                    break;
                }
                
                for (int j = 0; j < commands.Length; j++)  {
                    if (Array.Exists(commands[j], c => c == com[0].ToLower().Replace(" ", ""))) {
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
    }

    public void StopCode() {
        finished = false;
        start = false;
        ind = 0;
        naprList.Clear();
        SecondTask.StartLocation(car);
    }

    public void StartCode() {
        finished = false;
        ind = 0;
        per = 0f;
        naprList.Clear();
        start = true;
        SecondTask.StartLocation(car);
        if (isCorrect) {
            Debug.Log("Starting...");
            for (int i = 0; i < code.Length; i++) {
                string line = code[i].ToLower();
                string[] com = line.Split(" ");
                if (com.Length > 0) {
                    if (com.Length == 2) {
                        bool numb = int.TryParse(com[1], out n);
                    } else {
                        n = 1;
                    }

                    if (Array.Exists(commands[0], c => c == com[0])) {
                        for (int b = 0; b < n; b++) {
                            naprList.Add("up");
                        }
                    } else if (Array.Exists(commands[1], c => c == com[0])) {
                        for (int b = 0; b < n; b++) {
                            naprList.Add("down");
                        }
                    } else if (Array.Exists(commands[2], c => c == com[0])) {
                        for (int b = 0; b < n; b++) {
                            naprList.Add("left");
                        }
                    } else if (Array.Exists(commands[3], c => c == com[0])) {
                        for (int b = 0; b < n; b++) {
                            naprList.Add("right");
                        }
                    }
                }
            }
        } else {
            Debug.Log("Syntax error");
            Debug.Log(inCorrectLine);
        }
    }
}
