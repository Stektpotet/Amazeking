using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FollowAndKeepInLevel))]
public class FollowAndKeepInLevelEditor : Editor
{
	FollowAndKeepInLevel followScript;



	void OnEnable()
	{
		followScript = target as FollowAndKeepInLevel;
	}
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		if(GUILayout.Button("Set Camera Size"))
        {
			followScript.Awake();
			for(int i = 0; i < followScript.levelAreas.Count; i++)
			{
				if(followScript.levelAreas[i].levelRect.Contains(followScript.player.transform.position))
				{
					followScript.MoveToLevelArea(i);
				}
			}
			followScript.ClampCameraSize();
			followScript.ClampCameraPos();
		}
	}
	void OnSceneGUI ()
	{
		
		for(int i = 0; i < followScript.levelAreas.Count; i++)
		{
			EditorGUI.BeginChangeCheck();
			followScript.levelAreas[i].Set(HandlesUtil.ResizeRect(followScript.levelAreas[i].levelRect, Handles.DotCap, 1f));
			if(EditorGUI.EndChangeCheck())
			{
				Undo.RecordObject(target, "Changed levelBounds");
				EditorUtility.SetDirty(followScript);
			}
		}
	}
}

public static partial class HandlesUtil
{
	public static Rect ResizeRect(Rect rect, Handles.DrawCapFunction capFunc, float snap)
	{
		Vector3[] rectangleCorners =
			{
				rect.min,							// Bottom Left
                new Vector2(rect.xMax, rect.y),	// Bottom Right
				rect.max,							// Top Right
                new Vector2(rect.x, rect.yMax)	// Top Left
            };
		Vector2[] handlePoints =
			{
			//	rectangleCorners[0],								//Bottom Left
				new Vector2(rect.center.x, rect.y),	// Bottom 
			//	rectangleCorners[1],
                new Vector2(rect.xMax, rect.center.y),	// Right
			//	rectangleCorners[2],
                new Vector2(rect.center.x, rect.yMax),	// Top
			//	rectangleCorners[3],
                new Vector2(rect.x, rect.center.y)		// Left
            };


		Handles.Label(rectangleCorners[3]+Vector3.up, "Level Bounds");
		Handles.DrawSolidRectangleWithOutline(rectangleCorners,Color.green*0.2f,Color.green);

		for(int i = 0; i < handlePoints.Length; i++)
		{
			Vector2 p = handlePoints[i];
			handlePoints[i] = Handles.Slider(p, p - rect.center, 0.45f, capFunc, snap);
		}
		return new Rect(handlePoints[3].x, handlePoints[0].y, handlePoints[1].x - handlePoints[3].x, handlePoints[2].y - handlePoints[0].y);
	}
}




