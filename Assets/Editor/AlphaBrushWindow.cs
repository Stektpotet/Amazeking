using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

public class AlphaBrushWizard : ScriptableWizard
{
	string[] a = { "red", "green", "blue", "alpha" };

	PaintedMask mask;

	void OnEnable()
	{
		minSize = new Vector2(210f, 200f);
	}

	void OnDisable()
	{
		mask.isPainting = false;
	}

	public static void CreateWizard(PaintedMask mask)
	{
		AlphaBrushWizard wizard = DisplayWizard<AlphaBrushWizard>("AlphaBrush", "Apply");
		wizard.mask = mask;
	}
	void OnWizardCreate()
	{ }
	//Texture2D[] Brushes()
	//{
	//	List<Texture2D> t = new List<Texture2D>();
	//	t.AddRange(Resources.LoadAll<Texture2D>("Gizmos"));
	//	t.AddRange(Resources.FindObjectsOfTypeAll<Texture2D>().Where( x => x.format == TextureFormat.Alpha8));
	//	return t.ToArray();
	//}

	protected override bool DrawWizardGUI()
	{
		EditorGUI.BeginChangeCheck();
		EditorGUILayout.BeginHorizontal();
		PaintedMask.color = GUILayout.Toolbar(PaintedMask.color, a);
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.BeginVertical();
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Size", GUILayout.Width(48));
		PaintedMask.brushSize = EditorGUILayout.IntSlider(PaintedMask.brushSize, 1, 100);
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Opacity", GUILayout.Width(48));
		PaintedMask.opacity = EditorGUILayout.Slider(PaintedMask.opacity, 0f, 1f);
		EditorGUILayout.EndHorizontal();
		PaintedMask.color = GUILayout.Toggle(PaintedMask.color == 4, "Eraser", "Button") ? 4 : PaintedMask.color;
		
		if(GUILayout.Button("Clear Masks"))
		{
			if(EditorUtility.DisplayDialog("Clear Masks?", "This will clear all blendmaps!", "Clear", "Cancel"))
			{
				mask.ClearMasks();
			}
		}

		EditorGUILayout.EndVertical();
		return EditorGUI.EndChangeCheck();
	}
}
