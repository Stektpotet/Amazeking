using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class DoorInteraction : InteractionBase {

	public ListeMannInteraction waitForInteraction;

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
