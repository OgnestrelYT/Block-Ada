using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public GameObject winDialog;
    public bool flag = true;
    public Text text;
    public Button button;

    public string[] message;
    public int numDialog = 0;

    private void Start()
    {
        winDialog.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (numDialog + 2 > message.Length)
            {
                winDialog.SetActive(false);
                button.onClick.RemoveAllListeners();
                //numDialog = 0;
            }
            else
            {
                button.onClick.AddListener(NextDialog);
            }
            winDialog.SetActive(true);
            text.text = message[numDialog];
            flag = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            winDialog.SetActive(false);
            button.onClick.RemoveAllListeners();
            numDialog = 0;
        }
    }

    public void NextDialog()
    {
        if (numDialog + 2 > message.Length)
        {
            winDialog.SetActive(false);
            button.onClick.RemoveAllListeners();
            numDialog = 0;
        }
        numDialog++;
        text.text = message[numDialog];
    }
}
