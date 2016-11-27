using UnityEngine;
using System.Collections;

/// <summary>
/// The script adjusts the Canvas's scale factor so that it matches the PixelPerfectCamera's.
/// <para />
/// The Canvas render mode must be "Screen Space - Camera". The selected render camera should use the "PixelPerfectCamera" script.
/// </summary>
/// <remarks>
/// Even if you don't use this script, setting integer values to Canvas Scaler's scale factor will result in pixel perfect rendering. However, 
/// the canvas' scale will not match the camera's. Use this script to have the Canvas' scale match the PixelPerfectCamera's.
/// </remarks>
[ExecuteInEditMode]
[RequireComponent(typeof(Canvas))]
public class CanvasScreenSpaceScaler : MonoBehaviour {

	Canvas _canvas;
	PixelPerfectCamera _pixelPerfectCamera;

	void Initialize()
    {
		_canvas = GetComponent<Canvas> ();

		if (_canvas.renderMode != RenderMode.ScreenSpaceCamera) {
			
			Debug.Log("Render mode: " + _canvas.renderMode + " is not supported by CanvasScreenSpaceScaler");
			return;
		}
			
		Camera uiCamera = GetComponent<Canvas> ().worldCamera;

		_pixelPerfectCamera = uiCamera.GetComponent<PixelPerfectCamera> ();

        if (_pixelPerfectCamera == null)
        {
            Debug.Log("You have to use the PixelPerfectCamera script on the canvas' render camera!");
            return;
        }

        AdjustCanvas();
    }

    void OnEnable()
    {
        Initialize();
    }

    void OnValidate()
    {
        Initialize();
    }

	//#if UNITY_EDITOR
	void Update ()
    {
		if (_pixelPerfectCamera == null || _canvas.renderMode != RenderMode.ScreenSpaceCamera)
			return;

        // Detect changes in ratio
        if (_canvas.scaleFactor != _pixelPerfectCamera.ratio)
            AdjustCanvas();
    }
    //#endif

    void AdjustCanvas()
    {
        _canvas.scaleFactor = _pixelPerfectCamera.ratio;
    }
}
