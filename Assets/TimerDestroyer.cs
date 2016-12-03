using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerDestroyer : MonoBehaviour
{
	void OnEnable()
	{
		TimerManager timer = FindObjectOfType<TimerManager>();
		if(timer != null)
		{
			Destroy(timer);
		}
	}
}
