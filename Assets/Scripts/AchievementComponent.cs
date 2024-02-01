using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AchievementComponent : MonoBehaviour {

	[SerializeField] private Image _icon;
	[SerializeField] private Image _bg;
	[SerializeField] private Text _title;
	[SerializeField] private Text _description;

	public bool isActive { get; set; }

	void Show()
	{
		gameObject.SetActive(true);

		StartCoroutine(Wait(5)); // скрыть через 5 секунд
	}

	void Hide()
	{
		gameObject.SetActive(false);

		AchievementSystem.use.ShowNextAchievement(); // показать следующую открытую ачивку, если таковая есть
	}
	
	public void SetAchievement(Sprite icon, Sprite bg, string title, string description)
	{
		_icon.sprite = icon;
		_bg.sprite = bg;
		_title.text = title;
		_description.text = description;
		isActive = true;
		Show();
	}

	IEnumerator Wait(float t)
	{
		yield return new WaitForSeconds(t);
		Hide();
	}
}