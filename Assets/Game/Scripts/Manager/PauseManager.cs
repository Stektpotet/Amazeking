using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseManager : MonoBehaviour
{

	public Canvas canvas; //pause canvas

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Pause();
		}
	}

	public void Pause()
	{
		canvas.enabled = !canvas.enabled;
		Time.timeScale = (Time.timeScale == 0) ? 1 : 0;
	}

	public void Quit()
	{
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}
}