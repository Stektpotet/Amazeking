using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorInteraction : ScriptableInteraction {

	public InteractionBase waitForInteraction;

	protected override void Interaction()
	{
		if(waitForInteraction.interacted)
		{
			SceneManager.LoadSceneAsync("level2");
		}
		else
		{
			Debug.Log("You have not talked to ListeMann yet!");
		}
    }
}
