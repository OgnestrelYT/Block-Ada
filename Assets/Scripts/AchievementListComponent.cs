using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AchievementListComponent : MonoBehaviour {

	[SerializeField] private int _achievementID;
	[SerializeField] private RectTransform _rectTransform;
	[SerializeField] private Image _icon;
	[SerializeField] private Text _title;
	[SerializeField] private Text _description;
	[SerializeField] private Slider _progressBar;
	[SerializeField] private Text _progressText;

	public int achievementID
	{
		get{ return _achievementID; }
	}

	public RectTransform rectTransform
	{
		get{ return _rectTransform; }
	}

	public void CreateAchievement(int id, string title, string description)
	{
		_achievementID = id;
		_title.text = title;
		_description.text = description;
	}

	public void SetAchievement(Sprite icon, int targetValue, int currentValue)
	{
		_icon.sprite = icon;
		_progressText.text = currentValue + "/" + targetValue;
		_progressBar.value = (float)currentValue/(float)targetValue;
	}
}