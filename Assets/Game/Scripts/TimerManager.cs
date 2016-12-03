using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
	void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}

	public static float time = 0.0f;
	public Text timerText;
	bool stopped = false;

	void OnEnable()
	{
		if(GameManager.timed)
		{
			gameObject.SetActive(true);
		}
		else
		{
			gameObject.SetActive(false);
		}
	}

	void Update ()
	{
		if(!stopped)
		{
			time += Time.deltaTime;
			timerText.text = TimeFormat(time);
		}
	}

	public void Reset()
	{
		stopped = false;
		time = 0;
	}

	public string TimeFormat(float time)
	{
		int m = Mathf.RoundToInt(time) / 60;
		int s = (Mathf.RoundToInt(time)-m*60) - m;

		return m + "m " + s + "s";
	}

	public void StopTimer()
	{
		stopped = true;
	}

}
