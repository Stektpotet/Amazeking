using UnityEngine;
using UnityEngine.Events;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class Sky : MonoBehaviour
{
	public Gradient skyColorGradient;

	private static float m_distanceCoveredPercent = 0;
	public static float distanceCoveredPercent { get { return m_distanceCoveredPercent; } }
	public UnityEvent colorUpdator;

	static Color m_SkyColor;
	public static Color skyColor { get { return m_SkyColor; } }
	private static float xStart, distance = 1;
	public static float lastPercent;
	public static float distanceX { get { return distance * lastPercent + xStart; } }

	void OnValidate()
	{
		Collider2D c = GetComponent<Collider2D>();
		xStart = c.bounds.min.x;
		distance = Mathf.Max(c.bounds.size.x, 1);
	}
	public void UpdateSky(float x)
	{
		float percent = Mathf.Abs((xStart - x) / distance);
		lastPercent = percent;
		if(percent > m_distanceCoveredPercent)
		{
			m_distanceCoveredPercent = percent;
			m_SkyColor = skyColorGradient.Evaluate(percent);
		}
	}
	void OnTriggerStay2D(Collider2D other)
	{
		UpdateSky(other.transform.position.x);	
		colorUpdator.Invoke();
	}
}
