using UnityEngine;
using UnityEditor;
using System.Collections;

[CanEditMultipleObjects]
[CustomEditor(typeof(PaintedMask))]
public class PaintedMaskEditor : Editor
{
	SerializedProperty paintToggle;

	void OnEnable()
	{
		paintToggle = serializedObject.FindProperty("isPainting");
	}

	public override void OnInspectorGUI()
	{
		if(paintToggle.boolValue)
		{
			paintToggle.boolValue = GUILayout.Toggle(paintToggle.boolValue, "Painting", "Button");
			if(!paintToggle.boolValue)
			{
				EditorWindow.GetWindow<AlphaBrushWindow>().Close();
			}
		}
		else
		{
			paintToggle.boolValue = GUILayout.Button("Painting");
			if(paintToggle.boolValue)
			{
				AlphaBrushWindow.Initialize();
			}
		}
		
	}
}
