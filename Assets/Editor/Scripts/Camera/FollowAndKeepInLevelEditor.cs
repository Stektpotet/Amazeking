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
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		if(GUILayout.Button("Set Camera Size"))
        {
			followScript.Awake();
			followScript.ClampCameraSize();
			followScript.ClampCameraPos();
		}
	}
	void OnSceneGUI ()
	{
		EditorGUI.BeginChangeCheck();
		followScript.levelBounds = HandlesUtil.ResizeRect(followScript.levelBounds, Handles.DotCap,1f);
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
			handlePoints[i] = Handles.Slider(p, p - rect.center, HandleUtility.GetHandleSize(p)*0.05f, capFunc, snap);
		}
		return new Rect(handlePoints[3].x, handlePoints[0].y, handlePoints[1].x - handlePoints[3].x, handlePoints[2].y - handlePoints[0].y);
		
	}
	
}




