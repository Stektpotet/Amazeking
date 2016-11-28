using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
	public static void NextLevel()
	{
		SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex+1);
	}
	public static void LoadLevel(string levelName)
	{
		SceneManager.LoadSceneAsync(levelName);
	}
}
