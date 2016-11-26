using UnityEngine;
using System.Collections;

public class Sky : MonoBehaviour
{
	public Gradient skyColorGradient;
	float distanceCoveredPercent = 0;
	public static Color skyColor;

	public float xStart, distance=1;

	void OnValidate()
	{
		if(distance < 1) { distance = 1; }
	}

	public void UpdateSky(float x)
	{
		float percent = Mathf.Abs((xStart - x)/ distance);

		if(distance > distanceCoveredPercent)
		{
			skyColor = skyColorGradient.Evaluate(percent);
			GetComponent<SpriteRenderer>().color = skyColor;
		}
	}
	void OnTriggerStay2D(Collider2D other)
	{
		UpdateSky(other.transform.position.x);
	}
}
