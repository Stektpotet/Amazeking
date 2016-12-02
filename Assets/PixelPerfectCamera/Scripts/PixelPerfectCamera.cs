using UnityEngine;
using System.Collections;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

/**
 *  Adjusts the Camera component's orthographic size according to the supplied targeted size. If PixelPerfect is enabled, 
 *  the camera's pixels per unit will be set to a multiple of the assets' pixels per unit. Thus, an asset pixel will
 *  render to an integer number of screen pixels.
 *  
 *  In order to get pixel perfect result, point-sampling must be used in textures. Using linear filtering even in a 1-1 mapping, can be blurry. 
 *  For example if your sprites have a center pivot and the screen has a non-multiple of 2 dimension or if you translate your sprites
 *  in sub-pixel values. Using point-sampling solves all these issues.
 */
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class PixelPerfectCamera : MonoBehaviour {

    public static int PIXELS_PER_UNIT = 16;

    public enum Dimension {Width, Height};
    public enum ConstraintType {None, Horizontal, Vertical};

    // Input
    public bool maxCameraHalfWidthEnabled = true;
    public bool maxCameraHalfHeightEnabled = true;
    public float maxCameraHalfWidth = 40;
    public float maxCameraHalfHeight = 20.0f;
    public Dimension targetDimension = Dimension.Height;
    public float targetCameraHalfWidth = 22.0f;
    public float targetCameraHalfHeight = 10f;
    public bool pixelPerfect = true;
    public bool retroSnap = false;
    public float assetsPixelsPerUnit = PIXELS_PER_UNIT;
    public bool showHUD;

    // Output
    public Vector2 cameraSize;
    public ConstraintType contraintUsed;
    public float cameraPixelsPerUnit;
    public float ratio;
    public Vector2 nativeAssetResolution;
    public float fovCoverage;

    // Internal
    Resolution res;
    Camera cam;

	//void testMethod()
	//{
	//    float assetsPixelsPerUnit = 100;
	//    float assetWidth = 300;
	//    Resolution resolution = new Resolution();
	//    resolution.width = 1080;
	//    resolution.height = 1920;
	//    calculatePixelPerfectCameraSize(resolution, assetsPixelsPerUnit, assetWidth / assetsPixelsPerUnit / 2, -1);
	//}

	//res, assetsPixelsPerUnit, maxCameraHalfWidthReq, maxCameraHalfHeightReq, targetCameraHalfWidth, targetCameraHalfHeight, targetDimension

	public float CalculatePixelPerfectCameraSize()
    {
		float maxCameraHalfWidthReq = ( maxCameraHalfWidthEnabled ) ? maxCameraHalfWidth : -1;
		float maxCameraHalfHeightReq = ( maxCameraHalfHeightEnabled ) ? maxCameraHalfHeight : -1;

		float maxHorizontalFOV = 2f * maxCameraHalfWidthReq;
        float maxVerticalFOV = 2f * maxCameraHalfHeightReq;
        float targetWidth = 2f * targetCameraHalfWidth;
        float targetHeight = 2f * targetCameraHalfHeight;
        float AR = (float)res.width / res.height;

        // How many screen pixels will an asset pixel render to?
        // or how many times will the asset dimensions be multiplied?
        float ratioTarget;
        
        if (targetDimension == Dimension.Width)
        {
            float assetsWidth = assetsPixelsPerUnit * targetWidth;
            ratioTarget = (float)res.width / assetsWidth;
        }
        else
        {
            float assetsHeight = assetsPixelsPerUnit * targetHeight;
            ratioTarget = (float)res.height / assetsHeight;        
        }
        float ratioTargetOriginal = ratioTarget;
        if (pixelPerfect)
        {
            float ratioSnapped = Mathf.Ceil(ratioTarget);
            float ratioSnappedPrevious = ratioSnapped - 1;
            // choose the ratio whose fov (or native asset resolution) is nearest to the ratioTarget's fov
            ratioTarget = (1/ratioTarget - 1 / ratioSnapped < 1/ratioSnappedPrevious - 1 / ratioTarget) ? ratioSnapped : ratioSnappedPrevious;
            if (ratioSnapped <= 1)
            {
                ratioTarget = 1;
            }
        }

        float ratioHorizontal = 0;
        float ratioVertical = 0;
        if (maxHorizontalFOV > 0f)
        {
            float assetsWidth = assetsPixelsPerUnit * maxHorizontalFOV;
            ratioHorizontal = (float)res.width / assetsWidth;
        }
        if (maxVerticalFOV > 0f)
        {
            float assetsHeight = assetsPixelsPerUnit * maxVerticalFOV;
            ratioVertical = (float)res.height / assetsHeight;
        }  
        float ratioMin = Mathf.Max(ratioHorizontal, ratioVertical);
        if (pixelPerfect)
        {
            ratioMin = Mathf.Ceil(ratioMin);
        }
        float ratioUsed = Mathf.Max(ratioMin, ratioTarget);

        float horizontalFOV = res.width / (assetsPixelsPerUnit * ratioUsed);
        float verticalFOV = horizontalFOV / AR;

        // ------ GUI Calculations  -----
        this.cameraSize = new Vector2(horizontalFOV / 2, verticalFOV / 2);
        bool unconstrained = ratioTarget >= Mathf.Max(ratioHorizontal, ratioVertical) && ratioTargetOriginal >= Mathf.Max(ratioHorizontal, ratioVertical);
        this.contraintUsed = (unconstrained) ? ConstraintType.None : (ratioHorizontal > ratioVertical) ? ConstraintType.Horizontal : ConstraintType.Vertical;
        this.cameraPixelsPerUnit = (float)res.width / horizontalFOV;
        this.ratio = ratioUsed;
        this.nativeAssetResolution = new Vector2(horizontalFOV * assetsPixelsPerUnit, verticalFOV * assetsPixelsPerUnit);
        this.fovCoverage = ratioTargetOriginal / ratioUsed;
        // ------ GUI Calculations End  -----

        return verticalFOV / 2;
    }

    public void adjustCameraFOV()
    {
        if (cam == null)
        {
            cam = GetComponent<Camera>();
        }
        res = new Resolution();
        res.width = cam.pixelWidth;
        res.height = cam.pixelHeight;
        res.refreshRate = Screen.currentResolution.refreshRate;
        float cameraSize = CalculatePixelPerfectCameraSize();	
        cam.orthographicSize = cameraSize;
    }

	// Use this for initialization
	void Start()
	{
		//testMethod();
		adjustCameraFOV();
	}

	void OnEnable()
	{
		adjustCameraFOV();
	}


	void OnValidate()
	{
		adjustCameraFOV();
	}

	public void UpdatePixlePerfectCamera()
	{
        if (res.width != cam.pixelWidth || res.height != cam.pixelHeight)
        {
            adjustCameraFOV();
        }
	}


    // http://docs.unity3d.com/Manual/gui-Basics.html
    void OnGUI () {
        if (showHUD)
        {
            float scale = Screen.dpi / 96.0f;

            // Make a background box
            GUIStyle boxStyle = new GUIStyle(GUI.skin.box);
            boxStyle.fontSize = (int)(13 * scale);
            GUI.Box(new Rect(10 * scale, 10 * scale, 130 * scale, 90 * scale), "Camera", boxStyle);

            // Make the first button. If it is pressed, update
            // http://forum.unity3d.com/threads/toggle-size.55615/
            GUIStyle toggleStyle = new GUIStyle(GUI.skin.toggle);
            toggleStyle.fontSize = (int)(13 * scale);
            toggleStyle.border = new RectOffset(0, 0, 0, 0);
            toggleStyle.overflow = new RectOffset(0, 0, 0, 0);
            toggleStyle.padding = new RectOffset(0,0,0,0);
            toggleStyle.imagePosition = ImagePosition.ImageOnly;
            pixelPerfect = GUI.Toggle(new Rect(20 * scale, 40 * scale, 20 * scale, 20 * scale), pixelPerfect, new GUIContent("Pixel perfect"), toggleStyle);
            GUIStyle labelStyle = new GUIStyle(GUI.skin.label);
            labelStyle.fontSize = (int)(13 * scale);
            GUI.Label(new Rect(40 * scale, 40 * scale, 80 * scale, 20 * scale), new GUIContent("Pixel perfect"), labelStyle);
            retroSnap = GUI.Toggle(new Rect(20 * scale, 60 * scale, 20 * scale, 20 * scale), retroSnap, new GUIContent("Retro Snap"), toggleStyle);
            GUI.Label(new Rect(40 * scale, 60 * scale, 80 * scale, 20 * scale), new GUIContent("Retro Snap"), labelStyle);
            if (GUI.changed)
            {
                adjustCameraFOV();
            }

        }
    }

}

#if UNITY_EDITOR
[CustomEditor(typeof(PixelPerfectCamera))]
[CanEditMultipleObjects]
public class PixelPerfectCameraEditor : Editor
{
    SerializedProperty maxCameraHalfWidthEnabled;
    SerializedProperty maxCameraHalfHeightEnabled;
    SerializedProperty maxCameraHalfWidth;
    SerializedProperty maxCameraHalfHeight;
    SerializedProperty targetDimension;
    SerializedProperty targetCameraHalfWidth;
    SerializedProperty targetCameraHalfHeight;
    SerializedProperty pixelPerfect;
    SerializedProperty retroSnap;
    SerializedProperty assetsPixelsPerUnit;
    SerializedProperty showHUD;

    void OnEnable()
    {
        maxCameraHalfWidthEnabled = serializedObject.FindProperty("maxCameraHalfWidthEnabled");
        maxCameraHalfHeightEnabled = serializedObject.FindProperty("maxCameraHalfHeightEnabled");
        maxCameraHalfWidth = serializedObject.FindProperty("maxCameraHalfWidth");
        maxCameraHalfHeight = serializedObject.FindProperty("maxCameraHalfHeight");
        targetDimension = serializedObject.FindProperty("targetDimension");
        targetCameraHalfWidth = serializedObject.FindProperty("targetCameraHalfWidth");
        targetCameraHalfHeight = serializedObject.FindProperty("targetCameraHalfHeight");
        pixelPerfect = serializedObject.FindProperty("pixelPerfect");
        retroSnap = serializedObject.FindProperty("retroSnap");
        assetsPixelsPerUnit = serializedObject.FindProperty("assetsPixelsPerUnit");
        showHUD = serializedObject.FindProperty("showHUD");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // Targeted Size
        PixelPerfectCamera.Dimension dimensionType = (PixelPerfectCamera.Dimension)Enum.GetValues(typeof(PixelPerfectCamera.Dimension)).GetValue(targetDimension.enumValueIndex);
		GUI.enabled = false;
		targetDimension.enumValueIndex = (int)(PixelPerfectCamera.Dimension)EditorGUILayout.EnumPopup("Target size", dimensionType);
        if (targetDimension.enumValueIndex == (int)PixelPerfectCamera.Dimension.Width)
        {
            EditorGUILayout.PropertyField(targetCameraHalfWidth, new GUIContent("Width", "The targetted half width of the camera."));
        }
        else
        {
            EditorGUILayout.PropertyField(targetCameraHalfHeight, new GUIContent("Height", "The targetted half height of the camera."));
        }
        EditorGUILayout.BeginHorizontal();
        maxCameraHalfWidthEnabled.boolValue = EditorGUILayout.Toggle(maxCameraHalfWidthEnabled.boolValue, GUILayout.Width(12));
        EditorGUI.BeginDisabledGroup(!maxCameraHalfWidthEnabled.boolValue);
        EditorGUILayout.PropertyField(maxCameraHalfWidth, new GUIContent("Max Width", "The maximum allowed half width of the camera."));
        EditorGUI.EndDisabledGroup();
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        maxCameraHalfHeightEnabled.boolValue = EditorGUILayout.Toggle(maxCameraHalfHeightEnabled.boolValue, GUILayout.Width(12));
        EditorGUI.BeginDisabledGroup(!maxCameraHalfHeightEnabled.boolValue);
        EditorGUILayout.PropertyField(maxCameraHalfHeight, new GUIContent("Max Height", "The maximum allowed half height of the camera."));
        EditorGUI.EndDisabledGroup();
        EditorGUILayout.EndHorizontal();
		
        // Pixels Per Unit
        EditorGUILayout.PropertyField(assetsPixelsPerUnit);

        // Pixel Perfect toggle
        pixelPerfect.boolValue = EditorGUILayout.Toggle(new GUIContent("Pixel Perfect", 
            "Makes the camera's pixels per unit to be a multiple of the assets' pixels per unit."), pixelPerfect.boolValue);
        
        // RetroSnap toggle
        retroSnap.boolValue = EditorGUILayout.Toggle(new GUIContent("Retro Snap",
            "Makes the objects using the PixelSnap script snap to the asset's pixel grid"), retroSnap.boolValue);
		GUI.enabled = true;
		// Show HUD toggle
		EditorGUILayout.PropertyField(showHUD);

        serializedObject.ApplyModifiedProperties();

        // Show results
        GUILayout.BeginVertical();
        GUILayout.Space(5);
        DrawSizeStats();
        GUILayout.EndVertical();
    }

    private void DrawSizeStats()
    {
        PixelPerfectCamera myCamera = (PixelPerfectCamera)target;
        EditorGUI.BeginDisabledGroup(true);

        // Size
        {
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.richText = true;
            string width = string.Format("{0:0.00}", myCamera.cameraSize.x);
            string height = string.Format("{0:0.00}", myCamera.cameraSize.y);
            if (myCamera.contraintUsed == PixelPerfectCamera.ConstraintType.Horizontal)
            {
                width = makeBold(width);
            }
            else if (myCamera.contraintUsed == PixelPerfectCamera.ConstraintType.Vertical)
            {
                height = makeBold(height);
            }
            EditorGUILayout.LabelField("Size", string.Format("{0} x {1}", width, height), style);
        }

        // Pixels Per Unit
        {
            string ppuString = string.Format("{0:0.00}", myCamera.cameraPixelsPerUnit);
            string tooltipString = "The number of screen pixels a unit is rendered to.";
            EditorGUILayout.LabelField(new GUIContent("Pixels Per Unit", tooltipString), new GUIContent(ppuString));
        }

        // Ratio
        {
            string ratioFormat = (myCamera.pixelPerfect) ? "{0:0}" : "{0:0.0000}";
            string pixelsString = string.Format(ratioFormat + "x [{1:0.00} x {2:0.00}]", myCamera.ratio, myCamera.nativeAssetResolution.x, myCamera.nativeAssetResolution.y);
            string tooltipString = "The screen resolution as a multiple of 2 numbers. The first is the number of screen pixels an asset pixel will render to. The second is the camera resolution in asset pixels.";
            EditorGUILayout.LabelField(new GUIContent("Pixels", tooltipString), new GUIContent(pixelsString));
        }

        // Target Coverage
        {
            string percentageUsed = string.Format("{0:P2}", myCamera.fovCoverage);
            string tooltipString = "How much of the targeted camera size was covered.";
            EditorGUILayout.LabelField(new GUIContent("Coverage", tooltipString), new GUIContent(percentageUsed));
        }

        EditorGUI.EndDisabledGroup();
    }

    private string makeBold(string str)
    {
        return "<b>" + str + "</b>";
    }


}
#endif
