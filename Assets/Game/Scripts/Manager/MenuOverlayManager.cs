using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuOverlayManager : MonoBehaviour
{
	bool gameOver = false, paused = false;

	public Canvas canvas; //pause canvas
	public GameObject gameOverMenu, pauseMenu;

	void Update()
	{
		if(!gameOver && Input.GetKeyDown(KeyCode.Escape))
		{
			TogglePause();
		}
	}

	public void GameOver()
	{
		Time.timeScale = ( Time.timeScale == 0 ) ? 1 : 0;
		canvas.gameObject.SetActive(true);
		gameOver = true;
		gameOverMenu.SetActive(true);
	}

	public void CloseGameOverMenu()
	{
		Time.timeScale = 1f;
		canvas.enabled = false;
		gameOver = false;
		gameOverMenu.SetActive(false);
	}

	public void TogglePause()
	{
		Time.timeScale = ( Time.timeScale == 0 ) ? 1 : 0;
		paused = ( Time.timeScale == 0 );
		canvas.gameObject.SetActive(paused);
		pauseMenu.SetActive(paused);
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