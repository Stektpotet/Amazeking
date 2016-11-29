using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[RequireComponent(typeof(Camera), typeof(RectTransform))]
public class FollowAndKeepInLevel : MonoBehaviour
{
	[System.Serializable]
	public class LevelArea
	{
		public int prefferedCamSize = 5;
		public Rect levelRect;

		public float width { get { return levelRect.width; } set { levelRect.width = value; } }
		public float height { get { return levelRect.height; } set { levelRect.height = value; } }
		public float x { get { return levelRect.x; } set { levelRect.x = value; } }
		public float y { get { return levelRect.y; } set { levelRect.y = value; } }
		public float xMax { get { return levelRect.xMax; } set { levelRect.xMax = value; } }
		public float yMax { get { return levelRect.yMax; } set { levelRect.yMax = value; } }

		public void Set(Rect r)
		{
			levelRect = r;
		}
	}


	public AnimationCurve interpolationCurve;
	public float interpotlationTime;

	public List<LevelArea> levelAreas = new List<LevelArea>();

	public int m_CurrentLevelArea = 0;
	public int CurrentLevelArea { get { return m_CurrentLevelArea; } }
	bool transitioning = false;

	public Transform player;
	public Vector2 focusOffset;

	private Camera cam;
	private PixelPerfectCamera pixPerfCam;

	public void Awake()
	{
		cam = GetComponent<Camera>();
		pixPerfCam = GetComponent<PixelPerfectCamera>();
	}

	void Start()
	{
		pixPerfCam.UpdatePixlePerfectCamera();
		ClampCameraSize();
	}

	void LateUpdate()
	{
		if(!transitioning)
		{
			pixPerfCam.UpdatePixlePerfectCamera();
			ClampCameraPos(ClampedCameraPos(cam.orthographicSize));
		}
	}

	public void MoveToLevelArea(int i)
	{
		if(i != m_CurrentLevelArea || transitioning)
		{
			m_CurrentLevelArea = i;
			StartCoroutine(ChangingLevelArea());
		}
	}

	IEnumerator ChangingLevelArea()
	{
		float lerpTime = 0;
		transitioning = true;
		float targetOrthoSize = ClampedCameraSize();
		Vector3 target = ClampedCameraPos(targetOrthoSize);
		while(Vector3.Distance(cam.transform.position, target) > 0.05f)
		{
			lerpTime += Time.deltaTime;
			target = ClampedCameraPos(targetOrthoSize);
			cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetOrthoSize, interpolationCurve.Evaluate(lerpTime / interpotlationTime));
			cam.transform.position = Vector3.Lerp(cam.transform.position, target, interpolationCurve.Evaluate(lerpTime / interpotlationTime));
			yield return null;
		}
		cam.orthographicSize = targetOrthoSize;
		pixPerfCam.maxCameraHalfHeight = cam.orthographicSize;
		pixPerfCam.maxCameraHalfWidth = cam.orthographicSize * cam.aspect;
		transitioning = false;
		StopCoroutine(ChangingLevelArea());
	}
	public void ClampCameraSize()
	{
		pixPerfCam.targetCameraHalfHeight = levelAreas[m_CurrentLevelArea].prefferedCamSize;
		pixPerfCam.UpdatePixlePerfectCamera();
		cam.orthographicSize = ClampedCameraSize();
	}

	public float ClampedCameraSize()
	{
		float size = levelAreas[m_CurrentLevelArea].prefferedCamSize;
		if(size * 2 > levelAreas[m_CurrentLevelArea].height)
		{
			size = levelAreas[m_CurrentLevelArea].height * 0.5f;
		}
		if(Mathf.Pow(cam.aspect, -1) * size * 2 > levelAreas[m_CurrentLevelArea].width)
		{
			size = levelAreas[m_CurrentLevelArea].width / cam.aspect;
		}
		return size;

	}
	public void ClampCameraPos()
	{
		cam.transform.position = ClampedCameraPos(cam.orthographicSize);
	}
	public void ClampCameraPos(Vector3 clampedCamPos)
	{
		cam.transform.position = clampedCamPos;
	}

	Vector3 ClampedCameraPos(float orthoSize)
	{
		float leftDist = levelAreas[m_CurrentLevelArea].x + cam.aspect * orthoSize;
		float rightDist = levelAreas[m_CurrentLevelArea].xMax - cam.aspect * orthoSize;
		float bottomDist = levelAreas[m_CurrentLevelArea].y + orthoSize;
		float topDist = levelAreas[m_CurrentLevelArea].yMax - orthoSize;

		float clampedX = Mathf.Clamp(player.transform.position.x + focusOffset.x, leftDist, rightDist);
		float clampedY = Mathf.Clamp(player.transform.position.y + focusOffset.y, bottomDist, topDist);
		return new Vector3(clampedX, clampedY, cam.transform.position.z);
	}
}
