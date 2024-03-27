using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObjForMap : MonoBehaviour
{

    [SerializeField] private Image _image;
    [SerializeField] private GameObject _gameObject;


    void Show()
	{
		gameObject.SetActive(true);
	}

	void Hide()
	{
		gameObject.SetActive(false);
	}
	
	public void SetAchievement(Sprite image, float x, float y)
	{
		_image.sprite = image;
		_gameObject.transform.posX = x;
		_gameObject.transform.posY = y;
		isActive = true;
		Show();
	}
}
