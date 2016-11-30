using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
	public Sprite sprite;

	public void OpenDoor()
	{
		GetComponent<SpriteRenderer>().sprite = sprite;
		foreach(Collider2D c in GetComponents<Collider2D>())
		{
			c.enabled = false;
		}
	}
}
