using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class OpeningDoor : MonoBehaviour
{
    public GameObject doorOpenMenu;
    public Button button;

    private void Start()
    {
        doorOpenMenu.SetActive(false);
        button.onClick.RemoveAllListeners();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (Dialogue.finish == true)
            {
            doorOpenMenu.SetActive(true);
            button.onClick.AddListener(OpenDoor);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            doorOpenMenu.SetActive(false);
            button.onClick.RemoveAllListeners();
        }
    }

    public void OpenDoor()
    {
        SceneManager.LoadScene("Down floors");
    }
}
