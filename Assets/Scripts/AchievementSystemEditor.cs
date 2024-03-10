#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(AchievementSystem))]

public class AchievementSystemEditor : Editor {

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		AchievementSystem e = (AchievementSystem)target;

		GUILayout.Label("Генерировать список ачивок:", EditorStyles.boldLabel);
		if(GUILayout.Button("Create / Update"))
		{
			e.CreateInEditor();
		}

		GUILayout.Label("Обнулить ачивки:", EditorStyles.boldLabel);
		if(GUILayout.Button("Обнулить ачивки"))
		{
			e.ToZero();
		}
	}
}
#endif