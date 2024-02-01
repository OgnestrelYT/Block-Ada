using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AchievementComponent : MonoBehaviour {

	[SerializeField] private Image _icon;
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
	
	public void SetAchievement(Sprite icon, string title, string description)
	{
		_icon.sprite = icon;
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