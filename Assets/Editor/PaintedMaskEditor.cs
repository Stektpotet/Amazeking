using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(PaintedMask))]
public class PaintedMaskEditor : Editor
{
	PaintedMask maskMaker;
	
	Projector m_brushProjector;
	public Projector brushProjector
	{
		get
		{
			if(m_brushProjector == null)
			{ CreateProjector(); }
			return m_brushProjector;
		}
	}

	void CreateProjector()
	{
		GameObject projectorObj = new GameObject("brushProjector");
		projectorObj.hideFlags = HideFlags.HideAndDontSave;

		m_brushProjector = projectorObj.AddComponent<Projector>();
		m_brushProjector.orthographic = true;
		m_brushProjector.orthographicSize = 1;
		m_brushProjector.material = Resources.Load<Material>("Editor/Materials/LightProjector");
		projectorObj.transform.position = Camera.main.transform.position;
	}
	
	void OnEnable()
	{
		maskMaker = target as PaintedMask;
		brushProjector.enabled = false;
		
	}
	void OnDisable()
	{
		DestroyImmediate(brushProjector.gameObject);
	}

	public override void OnInspectorGUI()
	{
		EditorGUI.BeginChangeCheck();
		maskMaker.resolutionMultiplier = EditorGUILayout.IntField("Resolution Multiplier", maskMaker.resolutionMultiplier);
		EditorGUILayout.LabelField("Resolution: " + maskMaker.maskTex.width + " x " + maskMaker.maskTex.height);
		if(EditorGUI.EndChangeCheck())
		{
			maskMaker.UpdateTextureSize();
		}
		if(!maskMaker.isPainting)
		{
			if(GUILayout.Button("Paint"))
			{
				maskMaker.isPainting = true;

				AlphaBrushWizard.CreateWizard(maskMaker);
			}
		}
		EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
		EditorGUILayout.LabelField("RGB", GUILayout.Width(40));
		GUI.DrawTexture(GUILayoutUtility.GetRect(100f, 100f), maskMaker.maskTex, ScaleMode.ScaleToFit, false);
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
		EditorGUILayout.LabelField("Alpha", GUILayout.Width(40));
		GUI.DrawTexture(GUILayoutUtility.GetRect(100f, 100f), maskMaker.maskTex, ScaleMode.ScaleToFit, true);
		EditorGUILayout.EndHorizontal();
		brushProjector.enabled = maskMaker.isPainting;
	}

	void OnSceneGUI()
	{
		if(maskMaker.isPainting)
		{
			Event e = Event.current;
			int controlId = GUIUtility.GetControlID(FocusType.Passive);
			switch (e.type)
			{
				case EventType.MouseDown:
					goto case EventType.MouseDrag;
				case EventType.MouseDrag:
					maskMaker.Paint(HandleUtility.GUIPointToWorldRay(e.mousePosition).origin);
					brushProjector.transform.position = HandleUtility.GUIPointToWorldRay(e.mousePosition).origin;
					Repaint();
					e.Use();
					break;
				case EventType.MouseMove:
					SceneView.RepaintAll();
					brushProjector.orthographicSize = 2f*PaintedMask.brushSize/maskMaker.resolutionMultiplier;
					brushProjector.transform.position = HandleUtility.GUIPointToWorldRay(e.mousePosition).origin;
					break;
			}
			GUIUtility.hotControl = controlId;
		}
	}
	public override bool HasPreviewGUI()
	{
		return base.HasPreviewGUI();
	}
	public override void OnPreviewGUI(Rect r, GUIStyle background)
	{
		AssetPreview.GetAssetPreview(maskMaker.maskTex);
		base.OnPreviewGUI(r,background);
	}
}
