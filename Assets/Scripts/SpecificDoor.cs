using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpecificDoor : MonoBehaviour
{
    [Space]
    [Header("Настройки двери:")]
    [SerializeField] public bool isSceneChanger; // тип двери
    [SerializeField] public string scene1;
    [SerializeField] public string scene2;
    [SerializeField] public string scene3;

    [Space]
    [Header("Объекты:")]
    [SerializeField] public GameObject buttonPref; // префаб кнопки
    [SerializeField] public Transform parent; // объект кнопки

    [SerializeField, HideInInspector] public Button button; // кнопка
    [SerializeField, HideInInspector] public GameObject buttonObj; // объект кнопки
    [SerializeField, HideInInspector] public static bool interactionAllow; // можно ли открывать

    void Start()
    {
        buttonObj = Instantiate(buttonPref, new Vector3(960, 100, 0), Quaternion.identity, parent);
        button = buttonObj.GetComponent<Button>();
        buttonObj.SetActive(false);
        if (isSceneChanger) {
            button.onClick.AddListener(SceneChanger);
        }
    }

    public void SceneChanger() {
        Scenes.numAct = 0;
        if (PlayerPrefs.GetInt("work") == 1) {
            SceneManager.LoadScene(scene1);
        } else if (PlayerPrefs.GetInt("work") == 2) {
            SceneManager.LoadScene(scene2);
        } else if (PlayerPrefs.GetInt("work") == 3) {
            SceneManager.LoadScene(scene3);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.tag == "Player") && (interactionAllow)) {
            buttonObj.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player") {
            buttonObj.SetActive(false);
        }
    }
}
