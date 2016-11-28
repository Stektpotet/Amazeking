using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Trigger : MonoBehaviour
{
	public LayerMask acceptedLayers;

	public UnityEvent onEnter;
	public UnityEvent onStay;
	public UnityEvent onExit;

	public void ClearEnterTrigger()
	{
		onEnter.RemoveAllListeners();
	}
	public void ClearStayTrigger()
	{
		onStay.RemoveAllListeners();
	}
	public void ClearExitTrigger()
	{
		onExit.RemoveAllListeners();
	}

	public void OnTriggerEnter2D(Collider2D other)
	{
		if(( acceptedLayers & ( 1 << other.gameObject.layer ) ) > 0)
		{
			onEnter.Invoke();
		}
	}
	public void OnTriggerStay2D(Collider2D other)
	{
		if(( acceptedLayers & ( 1 << other.gameObject.layer ) ) > 0)
		{
			onStay.Invoke();
		}
	}
	public void OnTriggerExit2D(Collider2D other)
	{
		if(( acceptedLayers & ( 1 << other.gameObject.layer ) ) > 0)
		{
			onExit.Invoke();
		}
	}
}
