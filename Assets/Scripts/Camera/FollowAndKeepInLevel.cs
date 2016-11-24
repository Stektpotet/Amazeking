using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class FollowAndKeepInLevel : MonoBehaviour
{
	public Rect levelBounds;

	public Transform player;
	public Vector2 offset;

	public Camera cam;
	
	void Start()
	{
		cam = GetComponent<Camera>();
	}
	void LateUpdate()
	{
		float camHalfWidth = cam.aspect * cam.orthographicSize;
		float leftDist = levelBounds.x + camHalfWidth;
		float rightDist = levelBounds.xMax - camHalfWidth;
		float bottomDist = levelBounds.y + cam.orthographicSize;
		float topDist = levelBounds.yMax - cam.orthographicSize;

		float clampedX = Mathf.Clamp(player.transform.position.x + offset.x, leftDist, rightDist);
		float clampedY = Mathf.Clamp(player.transform.position.y + offset.y, bottomDist, topDist);
		cam.transform.position = new Vector3(clampedX, clampedY, cam.transform.position.z);
	}
	
	void OnValidate()
	{
		LateUpdate();
	}
}
