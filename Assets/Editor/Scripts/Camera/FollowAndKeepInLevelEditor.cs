using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FollowAndKeepInLevel))]
public class FollowAndKeepInLevelEditor : Editor
{
	FollowAndKeepInLevel followScript;

	SerializedProperty rectProp;


	void OnEnable()
	{
		followScript = target as FollowAndKeepInLevel;
		rectProp = serializedObject.FindProperty("levelBounds");
	}

	void OnSceneGUI ()
	{
		EditorGUI.BeginChangeCheck();
		followScript.levelBounds = HandlesUtil.ResizeRect(followScript.levelBounds, Handles.DotCap,0.25f);
		if(EditorGUI.EndChangeCheck())
		{
			Undo.RecordObject(target, "Changed levelBounds");
			EditorUtility.SetDirty(followScript);
		}
	}
}

public static partial class HandlesUtil
{
	public static Rect ResizeRect(Rect rect, Handles.DrawCapFunction capFunc, float snap)
	{
		Vector2 halfRectSize = new Vector2(rect.size.x * 0.5f, rect.size.y * 0.5f);
		Vector2[] rectangleCorners =
			{
				rect.min,							// Bottom Left
                new Vector2(rect.xMax, rect.y),	// Bottom Right
				rect.max,							// Top Right
                new Vector2(rect.x, rect.yMax)	// Bottom Right
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

		

		Handles.DrawAAPolyLine(rectangleCorners[0], rectangleCorners[1], rectangleCorners[2], rectangleCorners[3], rectangleCorners[0]);
		//Handles.DrawAAPolyLine(handlePoints[1], handlePoints[3], handlePoints[5], handlePoints[7], handlePoints[1]);
		for(int i = 0; i < handlePoints.Length; i++)
		{
			Vector2 p = handlePoints[i];
			handlePoints[i] = Handles.Slider(p, p - rect.center, HandleUtility.GetHandleSize(p)*0.05f, capFunc, snap);
		}
		return new Rect(handlePoints[3].x, handlePoints[0].y, handlePoints[1].x - handlePoints[3].x, handlePoints[2].y - handlePoints[0].y);
		
	}
}