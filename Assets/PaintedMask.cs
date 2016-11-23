using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class PaintedMask : MonoBehaviour
{
	[SerializeField]
	private Texture2D m_maskTex;
	public Texture2D maskTex
	{
		get
		{
			if(m_maskTex == null)
			{
				m_maskTex = new Texture2D(2, 2, TextureFormat.RGBA4444, true);
				m_maskTex.wrapMode = TextureWrapMode.Clamp;
				m_maskTex.filterMode = FilterMode.Trilinear;
				UpdateTextureSize();
			}
			return m_maskTex;
		}
		set { m_maskTex = value; }
	}

	[SerializeField]
	public MaterialPropertyBlock m_propBlock;

	SpriteRenderer m_renderer;
	public SpriteRenderer spriteRenderer
	{
		get
		{
			if(m_renderer == null)
			{
				m_renderer = GetComponent<SpriteRenderer>();
			}
			return m_renderer;
		}
	}

	[HideInInspector]
	public bool isPainting;

	public int resolutionMultiplier = 1;
	
	private static Color[] colors = { new Color(1, 0, 0, 0), new Color(0, 1, 0, 0), new Color(0, 0, 1, 0), new Color(0, 0, 0, 1), new Color(0, 0, 0, 0) };

	public static int color;
	public static int brushSize = 3;
	public static float opacity = 1f;

	public Color GetColor(int i)
	{
		return colors[i] * opacity;
	}

	private Color blank { get { return colors[4]; } }

	void OnEnable()
	{
		m_propBlock = new MaterialPropertyBlock();
		m_propBlock.SetTexture("_BlendMask", m_maskTex);
		spriteRenderer.SetPropertyBlock(m_propBlock);
		spriteRenderer.sprite.texture.wrapMode = TextureWrapMode.Repeat;
		spriteRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
		spriteRenderer.receiveShadows = true;
	}

	void OnValidate()
	{
		resolutionMultiplier = Mathf.Max(1, resolutionMultiplier);
	}

	public void UpdateTextureSize()
	{
		m_maskTex.Resize(
			resolutionMultiplier * Mathf.RoundToInt(transform.localScale.x*spriteRenderer.sprite.bounds.size.x),
			resolutionMultiplier * Mathf.RoundToInt(transform.localScale.y*spriteRenderer.sprite.bounds.size.y));
		m_maskTex.Apply();
		ClearMasks();
		m_propBlock.Clear();
		m_propBlock.SetTexture("_BlendMask", m_maskTex);
		m_renderer.SetPropertyBlock(m_propBlock);
	}
	
	public void Paint(Vector2 pos)
	{
		Vector2 size = new Vector2(
			Mathf.RoundToInt(transform.localScale.x * spriteRenderer.sprite.bounds.size.x),
			Mathf.RoundToInt(transform.localScale.y * spriteRenderer.sprite.bounds.size.y));
		Vector2 texPos = ( pos - (Vector2)transform.position + size * 0.5f) * resolutionMultiplier;
		Circle(Mathf.RoundToInt(texPos.x), Mathf.RoundToInt(texPos.y), brushSize, GetColor(color));
		maskTex.Apply();
		m_propBlock.Clear();
		m_propBlock.SetTexture("_BlendMask", m_maskTex);
		m_renderer.SetPropertyBlock(m_propBlock);
	}
	
	void Circle(int cx, int cy, int r, Color col)
	{
		int x, y, px, nx, py, ny, d;

		for(x = 0; x <= r; x++)
		{
			d = (int)Mathf.Ceil(Mathf.Sqrt(r * r - x * x));
			for(y = 0; y <= d; y++)
			{
				px = cx + x;
				nx = cx - x;
				py = cy + y;
				ny = cy - y;

				maskTex.SetPixel(px, py, col);
				maskTex.SetPixel(nx, py, col);
				maskTex.SetPixel(px, ny, col);
				maskTex.SetPixel(nx, ny, col);

			}
		}
	}

	public void Erease(Vector2 pos)
	{
		int c = color;
		color = 4;
		Paint(pos);
		color = c;
	}
	public void ClearMasks()
	{
		for(int x = 0; x < maskTex.width; x++)
		{
			for(int y = 0; y < maskTex.height; y++)
			{
				maskTex.SetPixel(x, y, blank);
			}
		}
		maskTex.Apply();
		m_propBlock.Clear();
		m_propBlock.SetTexture("_BlendMask", m_maskTex);
		m_renderer.SetPropertyBlock(m_propBlock);
	}
	
}
