using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
	public Collider2D doorCollider;
	public Sprite sprite;

	public void OpenDoor()
	{
		doorCollider.enabled = false;
		GetComponent<SpriteRenderer>().sprite = sprite;
		foreach(Collider2D c in GetComponents<Collider2D>())
		{
			c.enabled = false;
		}
	}

}
