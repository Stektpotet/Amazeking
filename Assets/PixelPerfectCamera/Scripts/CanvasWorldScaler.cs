using UnityEngine;
using System.Collections;


/// <summary>
/// The script adjusts the Canvas's size so that it matches the UI camera's size.
/// <para />
/// The Canvas render mode must be "World Space". You should provide a UI camera that uses the "PixelPerfectCamera" script to the script.
/// </summary>
/// <remarks>
/// Even if you don't use this script, enabling the "pixel perfect" mode of the camera's "PixelPerfectCamera" script will result in a  
/// pixel perfect canvas. However, the size of the UI camera will not be the same as the camera's size. So, use this 
/// script to match the UI camera's size.
/// </remarks>
[ExecuteInEditMode]
[RequireComponent(typeof(Canvas))]
public class CanvasWorldScaler : MonoBehaviour {

    [Tooltip("A camera that uses the PixelPerfectCamera script")]
    [SerializeField]
    Camera uiCamera;

    PixelPerfectCamera _pixelPerfectCamera;
    Vector2 _cameraSize;
    float _assetsPixelsPerUnit;

    Canvas _canvas;

	void Initialize()
    {
        _canvas = GetComponent<Canvas> ();

		if (_canvas.renderMode != RenderMode.WorldSpace) {

			Debug.Log("Render mode: " + _canvas.renderMode + " is not supported by CanvasWorldScaler");
			return;
		}

		if (uiCamera == null) {

			Debug.Log ("You have to assign a UI camera first!");
			return;
		}

		_pixelPerfectCamera = uiCamera.GetComponent<PixelPerfectCamera> ();

        if (_pixelPerfectCamera == null)
        {
            Debug.Log("You have to use the PixelPerfectCamera script on the assigned UI camera first!");
            return;
        }

        AdjustCanvas();
    }

    void OnEnable()
    {
        Initialize();
    }

    void OnAwake()
    {
        Initialize();
    }

    //#if UNITY_EDITOR
    void Update ()
    {
		if (_pixelPerfectCamera == null || _canvas.renderMode != RenderMode.WorldSpace)
			return;

        // Detect changes in camera size
        if (_assetsPixelsPerUnit != _pixelPerfectCamera.assetsPixelsPerUnit || _cameraSize != _pixelPerfectCamera.cameraSize)
            AdjustCanvas();
    }
	//#endif

    void AdjustCanvas()
    {
        _cameraSize = _pixelPerfectCamera.cameraSize;
        _assetsPixelsPerUnit = _pixelPerfectCamera.assetsPixelsPerUnit;
        GetComponent<RectTransform>().sizeDelta = 2.0f * _assetsPixelsPerUnit * _cameraSize;

        Vector3 localScale = GetComponent<RectTransform>().localScale;
        localScale.x = 1 / _assetsPixelsPerUnit;
        localScale.y = 1 / _assetsPixelsPerUnit;
        GetComponent<RectTransform>().localScale = localScale;
    }

}
