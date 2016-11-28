using UnityEngine;
using System.Collections;

public class RepeatInteraction : ScriptableInteraction
{
	protected override void Interaction()
	{
		interacted = false;
	}
}
