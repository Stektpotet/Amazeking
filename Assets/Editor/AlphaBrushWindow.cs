using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

public class AlphaBrushWindow : EditorWindow
{

	const int xCount = 6;
	int color = 0, brush = 0;
	string[] a = { "red", "blue", "green", "alpha" };
	private Texture2D[] m_brushes;
	float radius = 3.0f;

	Texture2D[] brushes
	{
		get
		{
			if(m_brushes == null)
			{ m_brushes = Brushes(); }
			return m_brushes;
		}
	}

	Projector brushProjector;

	[MenuItem("Window/AlphaBrush")]
	public static void Initialize()
	{
		AlphaBrushWindow window = GetWindow<AlphaBrushWindow>(true, "AlphaBrush");
		window.maxSize = new Vector2(350, 400);
		window.minSize = new Vector2(350, 400);
		window.Show();

		window.OnOpen();
	}

	public void OnOpen()
	{
		//GameObject projectorObj = new GameObject("brushProjector");
		//projectorObj.hideFlags = HideFlags.DontSave;
		//brushProjector = projectorObj.AddComponent<Projector>();
		//brushProjector.orthographic = true;
		//brushProjector.material = Resources.Load<Material>("Editor/Materials/brushProjection");
	}

	Texture2D[] Brushes()
	{
		List<Texture2D> t = new List<Texture2D>();
		t.AddRange(Resources.LoadAll<Texture2D>("Gizmos"));
		t.AddRange(Resources.FindObjectsOfTypeAll<Texture2D>().Where( x => x.format == TextureFormat.Alpha8));
		return t.ToArray();
    }

	void OnGUI()
	{
		GUILayout.BeginHorizontal();
		color = GUILayout.Toolbar(color, a);
		GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
		for (int i = 0; i < brushes.Length; i++)
		{
			if ((i) % xCount == 0)
			{
				GUILayout.EndHorizontal();
				GUILayout.BeginHorizontal();
			}
			if(GUILayout.Toggle(brush == i ,brushes[i], "Button", GUILayout.Width(54), GUILayout.Height(54)))
			{
				brush = i;
				//brushProjector.material.SetTexture("_MainTex", brushes[i]);
			}
		}
		GUILayout.EndHorizontal();
	}

	void OnPostprocessTexture()
	{
		m_brushes = Brushes(); //update brushes if a texture is imported
	}

	void OnSceneGUI()
	{
		Event e = Event.current;
		RaycastHit rayHit;
		if(Physics.Raycast(Camera.current.ViewportPointToRay(e.mousePosition), out rayHit))
		{
			//brushProjector.gameObject.transform.position = new Vector3(rayHit.point.x, rayHit.point.y, brushProjector.gameObject.transform.position.z);
			List<Vector3> points = new List<Vector3>();
			for (int i = 0; i < 100; i++)
			{
				points.Add(rayHit.point -Vector3.forward*3 + Vector3.right * Mathf.Cos(i/ 25) + Vector3.up * Mathf.Sin(i/25));
			}
			Debug.Log("yay?");
			Handles.DrawAAPolyLine(points.ToArray());
        }
	}
	
	// Window has been selected
	void OnFocus()
	{
		// Remove delegate listener if it has previously
		// been assigned.
		SceneView.onSceneGUIDelegate -= this.OnSceneGUI;
		// Add (or re-add) the delegate.
		SceneView.onSceneGUIDelegate += this.OnSceneGUI;
	}

	void OnDestroy()
	{
		// When the window is destroyed, remove the delegate
		// so that it will no longer do any drawing.
		SceneView.onSceneGUIDelegate -= this.OnSceneGUI;
	}

	void OnSceneGUI(SceneView sceneView)
	{
		// Do your drawing here using Handles.
		Handles.BeginGUI();
		// Do your drawing here using GUI.
		Handles.EndGUI();
	}
}
