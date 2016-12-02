using UnityEngine.Events;
using UnityEngine;
public class DoorInteraction : ScriptableInteraction {

	bool locked = true;
	public override void Start()
	{
		base.Start();
	}

	public UnityEvent isLocked;
	public UnityEvent isOpen;

	public void Unlock()
	{
		locked = false;
	}

	public void Lock()
	{
		locked = true;
	}

	protected override void Interaction()
	{
		if(locked)
		{
			interacted = false;
			isLocked.Invoke();
		}
		else
		{
			isOpen.Invoke();
		}
		
	}
}
