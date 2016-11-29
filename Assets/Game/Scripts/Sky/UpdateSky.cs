using UnityEngine;

public class UpdateSky : MonoBehaviour
{
	SpriteRenderer r;
	Vector3 offset;
	void Start()
	{
		offset = transform.position;
		r = GetComponent<SpriteRenderer>();
	}

	public void UpdateColor()
	{
		r.material.color = Sky.skyColor;
	}

	public void MoveWithBackGround(float moveSpeed)
	{
		transform.position = offset + Vector3.right * Sky.lastPercent * moveSpeed;
	}
}
