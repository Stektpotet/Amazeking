using UnityEngine;

public class UpdateSky : MonoBehaviour
{
	SpriteRenderer r;
	void Start()
	{
		r = GetComponent<SpriteRenderer>();
	}

	public void UpdateColor()
	{
		r.material.color = Sky.skyColor;
	}
}
