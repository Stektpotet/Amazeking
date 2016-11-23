using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public abstract class InteractionBase : MonoBehaviour
{
	protected bool inTrigger = false;
	public bool interacted = false;

	protected Action interact;

	protected abstract void Interaction();

	public virtual void Start()
	{
		interact = Interaction;
	}

	void Update()
	{
		if(inTrigger)
		{
			if(Input.GetKeyDown(KeyCode.E))
			{
				interacted = true;
				interact();
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			inTrigger = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			inTrigger = false;
		}
	}
}
