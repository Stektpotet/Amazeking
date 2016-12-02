using UnityEngine.Events;
public class DoorInteraction : ScriptableInteraction {

	public bool locked;
	public override void Start()
	{
		base.Start();
	}

	public UnityEvent isLocked;
	public UnityEvent isOpen;

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
