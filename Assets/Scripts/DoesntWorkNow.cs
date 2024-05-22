using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoesntWorkNow : MonoBehaviour
{
    [SerializeField] public Button buttonX; // кнопка для закрытия X
    [SerializeField] public Button buttonOkey; // кнопка для закрытия ОКЕЙ
    [SerializeField] public GameObject window; // окошко
    [SerializeField] public GameObject blur; // невидимый блюр, чтобы кнопки не тыкались
    
    
    void Start()
    {
        buttonOkey.onClick.AddListener(Close);
        buttonX.onClick.AddListener(Close);
    }


    public void Close() {
        window.SetActive(false);
        blur.SetActive(false);
        AnyMenu.isTopicOpen = false;
    }
}
