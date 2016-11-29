using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;

public class DialogueInputHandler : MonoBehaviour
{
	public InputField inputField;
	
	public void ApplyName( PlayerStats stats )
	{
		stats.data.playerName = inputField.text;
	}

}
