using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DoorInteraction : ScriptableInteraction {

	public InteractionBase waitForInteraction;
	Scene activeScene;

	public override void Start()
	{
		base.Start();
		activeScene = SceneManager.GetActiveScene();
	}
   
	protected override void Interaction()
	{               //level1 -> level2:
        if (activeScene == SceneManager.GetSceneByName("level1"))
        {
            if (waitForInteraction.interacted)
            {
                SceneManager.LoadSceneAsync("level2");
            }
            else
            {
                Debug.Log("You have not talked to ListeMann yet!");
            }
        }
                    //level2 -> level3:
        else if (activeScene == SceneManager.GetSceneByName("level2"))
        {
            SceneManager.LoadSceneAsync("level3");
        }
    }
}
