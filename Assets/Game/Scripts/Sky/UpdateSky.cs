using UnityEngine;

public class UpdateSky : MonoBehaviour
{
	SpriteRenderer r;
	Vector3 offset;
	public AnimationCurve cloudColoring;
	void Start()
	{
		offset = transform.position;
		r = GetComponent<SpriteRenderer>();
	}

	public void UpdateColor()
	{
		r.sharedMaterial.color = Sky.skyColor;
	}

	public void UpdateCloudColor()
	{
		Color c = Sky.skyColor;
		r.sharedMaterial.color = Color.white - c * cloudColoring.Evaluate(Sky.distanceCoveredPercent);
	}

	public void MoveWithBackGround(float moveSpeed)
	{
		transform.position = offset + Vector3.right * Sky.lastPercent * moveSpeed;
	}
}
