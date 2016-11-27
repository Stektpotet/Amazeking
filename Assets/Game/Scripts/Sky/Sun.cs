using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class Sun : MonoBehaviour
{
	public float heightMax = 22, heightMin = -2;
	public AnimationCurve sunHeightCurve;
	public Gradient sunColor;
	Vector3 offset;

	SpriteRenderer r;
	Light sunLight;

	public void Awake()
	{
		offset = transform.position;
		sunLight = GetComponentInChildren<Light>();
		r = GetComponent<SpriteRenderer>();
	}

	public void MoveSun()
	{
		float x = Sky.distanceCoveredPercent;

		float sunHeightPercent = sunHeightCurve.Evaluate(x);
		Vector3 newPos = ( (heightMax - heightMin) * sunHeightPercent - heightMin  ) * Vector3.up + Sky.distanceX * Vector3.right + offset;
		transform.position = newPos;

		sunLight.intensity = sunHeightCurve.Evaluate(x);
		sunLight.color = sunColor.Evaluate(x);
		r.color = sunLight.color;
	}
}
