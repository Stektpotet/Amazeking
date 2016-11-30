using UnityEngine;
using System.Collections;

public class RepeatInteraction : ScriptableInteraction
{
	public override void OnDisable()
	{
		base.OnDisable();
	}
	protected override void Interaction()
	{
		interacted = false;
	}
}
