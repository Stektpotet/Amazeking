using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class FollowAndKeepInLevel : MonoBehaviour
{
	public Rect levelBounds;

	public Transform player;
	public Vector2 offset;

	public Camera cam;

	float camHalfWidth { get { return cam.aspect * cam.orthographicSize; } }

	void Start()
	{
		cam = GetComponent<Camera>();
		if(camHalfWidth*2 < levelBounds.width)
		{
			cam.orthographicSize = levelBounds.width*0.25f / cam.aspect;
		}
		if(cam.orthographicSize * 2 < levelBounds.height)
		{
			cam.orthographicSize = levelBounds.height * 0.5f;
		}
	}
	void LateUpdate()
	{
	
		float leftDist = levelBounds.x + camHalfWidth;
		float rightDist = levelBounds.xMax - camHalfWidth;
		float bottomDist = levelBounds.y + cam.orthographicSize;
		float topDist = levelBounds.yMax - cam.orthographicSize;

		float clampedX = Mathf.Clamp(player.transform.position.x + offset.x, leftDist, rightDist);
		float clampedY = Mathf.Clamp(player.transform.position.y + offset.y, bottomDist, topDist);
		cam.transform.position = new Vector3(clampedX, clampedY, cam.transform.position.z);
	}
	
}
