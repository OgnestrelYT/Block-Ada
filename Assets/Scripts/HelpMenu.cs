using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpMenu : MonoBehaviour
{
    public GameObject helpMenu;
    public Button closeB;
    public static bool isClosed;
    public static bool needShow;


    void Start()
    {
        needShow = false;
        isClosed = false;
        helpMenu.SetActive(false);
        closeB.onClick.AddListener(Close);
    }

    void Update()
    {
        if (needShow) {
            PauseMenu.canOpen = false;
            helpMenu.SetActive(true);
        } else {
            helpMenu.SetActive(false);
        }
    }

    public void Close() {
        Debug.Log("123097523890760349876-982345");
        PauseMenu.canOpen = true;
        isClosed = true;
        needShow = false;
        helpMenu.SetActive(false);
    }
}
