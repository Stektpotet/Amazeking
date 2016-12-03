using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTimer : MonoBehaviour
{
	public void EnableTimer()
	{
		GameManager.timed = true;
		TimerManager.time = 0;
	}
	public void DisableTimer()
	{
		GameManager.timed = false;
	}
}
