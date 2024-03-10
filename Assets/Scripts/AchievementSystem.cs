using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class AchievementSystem : MonoBehaviour {

	[Header("Редактирование:")]
	[SerializeField] private Achievement[] achievements; // настраиваемый вручную список ачивок
	[Header("Шаблоны:")]
	[SerializeField] private AchievementComponent messageSample; // шаблон для ачивки, которая показывается по достижению
	[SerializeField] private AchievementListComponent listSample; // шаблон для ачивки, которая генерируется в список
	[Header("Настройки:")]
	#if UNITY_EDITOR
	[SerializeField] private float offset = 10; // смещение между ачивками в списке
	#endif
	[SerializeField] private RectTransform listTransform; // трансформ, который будет содержать список
	[SerializeField] private AchievementListComponent[] list; // генерируемый в редакторе список (обязательная сериализация)

	private static AchievementSystem _internal;
	private static bool _active;
	private List<Achieve> achieveLast;

	[System.Serializable] struct Achievement
	{
		public bool isAchieved; // ачивка открыта или нет
		public string title; // заголовок, название
		public string description; // описание ачивки
		public int targetValue; // значение, которое надо получить, чтобы открыть ачивку
		public int currentValue; // текущее значение, достигнутое пользователем
		public Sprite locked; // спрайт, когда ачивка заблокирована
		public Sprite unlocked; // спрайт, если ачивка разблокирована
		public Sprite bg;
	}

	public static AchievementSystem use
	{
		get{ return _internal; }
	}

	public static bool isActive
	{
		get{ return _active; }
	}

	void Awake()
	{
		achieveLast = new List<Achieve>();
		_active = false;
		_internal = this;
		listSample.gameObject.SetActive(false);
		messageSample.gameObject.SetActive(false);
		listTransform.gameObject.SetActive(false);

		Load();
	}

	public void Save()
	{
		string content = string.Empty;

		foreach(Achievement achieve in achievements)
		{
			if(content.Length > 0) content += "|";
			if(achieve.isAchieved) content += achieve.isAchieved.ToString(); else content += achieve.currentValue.ToString();
		}

		PlayerPrefs.SetString("Achievements", content);
		PlayerPrefs.Save();
		Debug.Log(this + " сохранение прогресса ачивок.");
	}

	void Load()
	{
		if(!PlayerPrefs.HasKey("Achievements")) return;

		string[] content = PlayerPrefs.GetString("Achievements").Split(new char[]{'|'});

		if(content.Length == 0 || content.Length != achievements.Length) return;

		for(int i = 0; i < achievements.Length; i++)
		{
			int j = Parse(content[i]);

			if(j < 0)
			{
				achievements[i].currentValue = achievements[i].targetValue;
				achievements[i].isAchieved = true;
			}
			else
			{
				achievements[i].currentValue = j;
			}
		}
	}

	int Parse(string text)
	{
		int value;
		if(int.TryParse(text, out value)) return value;
		return -1;
	}

	public void ShowAchievementList(bool value)
	{
		if(value) // обновление списка, перед показом
		{
			for(int i = 0; i < achievements.Length; i++)
			{
				Sprite sprite = (achievements[i].isAchieved) ? achievements[i].unlocked : achievements[i].locked;
				list[i].SetAchievement(sprite, achievements[i].bg, achievements[i].targetValue, achievements[i].currentValue);
			}
		}

		_active = value;
		listTransform.gameObject.SetActive(value);
	}

	// id - индекс ачивки из списка
	// value - на сколько пунктов изменить
	public void AdjustAchievement(int id, int value)
	{
		if(achievements[id].isAchieved || id < 0 || id > achievements.Length) return;

		achievements[id].currentValue += value;

		if(achievements[id].currentValue < 0) achievements[id].currentValue = 0;

		if(achievements[id].currentValue >= achievements[id].targetValue)
		{
			achievements[id].currentValue = achievements[id].targetValue;
			achievements[id].isAchieved = true;

			if(!messageSample.isActive) // показываем ачивку, если в данный момент не показывается
			{
				messageSample.SetAchievement(achievements[id].unlocked, achievements[id].bg, achievements[id].title, achievements[id].description);
			}
			else // или запоминаем ачивку, чтобы показать позже
			{
				Achieve a = new Achieve();
				a.description = achievements[id].description;
				a.title = achievements[id].title;
				a.sprite = achievements[id].unlocked;
				a.bg = achievements[id].bg;
				achieveLast.Add(a);
			}
		}
	}

	struct Achieve
	{
		public string title;
		public string description;
		public Sprite sprite;
		public Sprite bg;
	}

	public void ShowNextAchievement() // поочередный показ ачивок, если было открыта сразу несколько
	{
		int j = -1;
		for(int i = 0; i < achieveLast.Count; i++)
		{
			j = i;
		}

		if(j < 0)
		{
			messageSample.isActive = false;
			return;
		}

		messageSample.SetAchievement(achieveLast[j].sprite, achieveLast[j].bg, achieveLast[j].title, achieveLast[j].description);
		achieveLast.RemoveAt(j);
	}

	#if UNITY_EDITOR
	public void CreateInEditor() // инструмент для создания списка
	{
		foreach(AchievementListComponent e in list)
		{
			if(e) DestroyImmediate(e.gameObject);
		}
		float step = listSample.rectTransform.sizeDelta.y + offset;
		float sizeY = step * achievements.Length;
		listTransform.sizeDelta = new Vector2(listSample.rectTransform.sizeDelta.x, sizeY);
		float posY = step * achievements.Length/2 + step/2;
		list = new AchievementListComponent[achievements.Length];
		for(int i = 0; i < achievements.Length; i++)
		{
			posY -= step;
			RectTransform tr = Instantiate(listSample.rectTransform) as RectTransform;
			tr.SetParent(listTransform);
			tr.localScale = Vector3.one;
			tr.anchoredPosition = new Vector2(0, posY);
			tr.gameObject.SetActive(true);
			tr.name = "Achievement_" + i;
			list[i] = tr.GetComponent<AchievementListComponent>();
			list[i].CreateAchievement(i, achievements[i].title, achievements[i].description);
		}
	}
	#endif

	public void ToZero() // инструмент для обнуления ачивок
	{
		for(int i = 0; i < achievements.Length; i++)
		{
			achievements[i].currentValue = 0;
			achievements[i].isAchieved = false;
		}
		Save();
	}
}