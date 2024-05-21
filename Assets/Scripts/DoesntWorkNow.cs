using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoesntWorkNow : MonoBehaviour
{
    [SerializeField] public Button buttonX; // кнопка для закрытия
    [SerializeField] public Button buttonOkey; // кнопка для закрытия
    [SerializeField] public GameObject window; // окошко
    
    
    void Start()
    {
        buttonOkey.onClick.AddListener(Close);
        buttonX.onClick.AddListener(Close);
    }


    public void Close() {
        window.SetActive(false);
    }
}
