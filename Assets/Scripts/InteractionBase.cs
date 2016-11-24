using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class InteractionBase : MonoBehaviour
{
	protected bool inTrigger = false;
	public bool interacted = false;

    [SerializeField]
	protected UnityEvent interactionEvent;

	void Update()
	{
		if(inTrigger)
		{
			if(Input.GetKeyDown(KeyCode.E))
			{
				interacted = true;
				interactionEvent.Invoke();
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
public abstract class ScriptableInteraction : InteractionBase
{
    
   

    protected UnityAction scriptedInteraction;

	protected abstract void Interaction();

	public virtual void Start()
	{
        
		scriptedInteraction = Interaction;
		interactionEvent.AddListener(scriptedInteraction);
	}

	void Update()
	{
		if(inTrigger)
		{
			if(Input.GetKeyDown(KeyCode.E))
			{
				interacted = true;
				interactionEvent.Invoke();
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