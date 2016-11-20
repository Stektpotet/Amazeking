using UnityEngine;
using System.Collections;

public class PaintedMask : MonoBehaviour
{

	Texture2D maskTex;
	
	public bool isPainting;

	public int resolutionMultiplier = 2;

	void OnValidate()
	{
		UpdateTextureSize();
	}

	void UpdateTextureSize()
	{
		maskTex = new Texture2D(
			resolutionMultiplier * Mathf.RoundToInt(transform.localScale.x),
			resolutionMultiplier * Mathf.RoundToInt(transform.localScale.y));
		maskTex.wrapMode = TextureWrapMode.Clamp;
		GetComponent<MeshRenderer>().sharedMaterial.SetTexture("_BlendMask", maskTex);

		for(int x = 0; x < maskTex.width; x++)
		{
			for(int y = 0; y < maskTex.height; y++)
			{
				maskTex.SetPixel(x, y, Color.red * ( (float)x / maskTex.width ) + Color.green * ( (float)y / maskTex.height ) - new Color(0, 0, 0, 1));
			}
		}
		maskTex.Apply();
	}
}
