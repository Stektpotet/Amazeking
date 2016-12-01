using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
	public void NextLevel()
	{
		SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
	}
	public void LoadLevel(string levelName)
	{
		SceneManager.LoadSceneAsync(levelName);
	}
}
