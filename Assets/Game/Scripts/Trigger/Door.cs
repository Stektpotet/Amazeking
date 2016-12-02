using UnityEngine;
using System.Collections;

public class Door : Button
{
	public Sprite closed { get { return up; } }
	public Sprite open { get { return down; } }

	public void OpenDoor()
	{
		GetComponent<SpriteRenderer>().sprite = down;
		foreach(Collider2D c in GetComponents<Collider2D>())
		{
			c.enabled = false;
		}
	}
	public void CloseDoor()
	{
		GetComponent<SpriteRenderer>().sprite = up;
		foreach(Collider2D c in GetComponents<Collider2D>())
		{
			c.enabled = true;
		}
	}
}
