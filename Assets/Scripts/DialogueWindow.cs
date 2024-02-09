using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueWindow : MonoBehaviour
{
    [SerializeField] public GameObject dialogueWin;
    [SerializeField] private Image _avatar;
	[SerializeField] private Image _bg;
	[SerializeField] private Text _name;
	[SerializeField] private Text _text;
    [HideInInspector, SerializeField] public string[] _messages;
    [SerializeField] public Button skipButton;
    [HideInInspector] public int numDialog = 0;
    public bool isActive { get; set; }


    void Show()
	{
		dialogueWin.SetActive(true);
        skipButton.onClick.AddListener(NextDialog);
        Debug.Log("Iphone");
	}

	void Hide()
	{
        numDialog = 0;
        skipButton.onClick.RemoveAllListeners();
		dialogueWin.SetActive(false);
        Scenes.canSkip = true;
	}

    public void SetDialogue(Sprite avatar, Sprite bg, string name, string[] messages)
	{
		_avatar.sprite = avatar;
		_bg.sprite = bg;
		_name.text = name;
        _messages = messages;
		isActive = true;
        _text.text = messages[numDialog];
		Show();
	}
    

    public void NextDialog()
    {
        if (numDialog + 1 >= _messages.Length) {
            Hide();
        } else {
            numDialog++;
            _text.text = _messages[numDialog];
        }
    }
}
