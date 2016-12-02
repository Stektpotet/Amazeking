using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
	public AudioClip newMusic;
	void Start()
	{
		if(newMusic != null && newMusic != GameManager.audioSource.clip)
		{
			GameManager.audioSource.clip = newMusic;
		}
	}

	public void NextLevel()
	{
		SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
	}
	public void LoadLevel(string levelName)
	{
		SceneManager.LoadSceneAsync(levelName);
	}

	public void Quit()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
	}
}
