using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class FollowAndKeepInLevel : MonoBehaviour
{
	public Rect levelBounds;

	public Transform player;
	public Vector2 offset;

	Camera cam;

	void Start()
	{
		cam = GetComponent<Camera>();
	}

	void LateUpdate()
	{
	}

}
