using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public GameObject winDialog;
    public bool stopted = false;
    public bool repeatAfterLeaving = true;
    public Text text;
    public Text nameText;
    public Button button;
    public Image avatarObj;
    public Sprite avatarImg;

    public string nameOfCharacter;

    public string[] message;
    public int numDialog = 0;

    private void Start()
    {
        winDialog.SetActive(false);
        nameText.text = nameOfCharacter;
        avatarObj.sprite = avatarImg;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (numDialog + 2 > message.Length)
            {
                if (repeatAfterLeaving)
                {
                    numDialog = 0;
                    button.onClick.AddListener(NextDialog);
                    text.text = message[numDialog];
                    winDialog.SetActive(true);
                } else {
                    winDialog.SetActive(false);
                    button.onClick.RemoveAllListeners();
                }
            }
            else
            {
                button.onClick.AddListener(NextDialog);
                text.text = message[numDialog];
                winDialog.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            winDialog.SetActive(false);
            button.onClick.RemoveAllListeners();
        }
    }

    public void NextDialog()
    {
        if (numDialog + 2 > message.Length)
        {
            winDialog.SetActive(false);
            button.onClick.RemoveAllListeners();
        }

        if (!stopted)
        {
            numDialog++;
        }
        text.text = message[numDialog];
    }
}
