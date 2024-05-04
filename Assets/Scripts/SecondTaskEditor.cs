#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(SecondTask))]

public class SecondTaskEditor : Editor {

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		SecondTask e = (SecondTask)target;

		GUILayout.Label("Подсказка в начале:", EditorStyles.boldLabel);
		if(GUILayout.Button("Обнулить"))
		{
			e.Firstly();
		}
	}
}
#endif