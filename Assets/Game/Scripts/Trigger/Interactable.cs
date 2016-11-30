using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
	public UnityEvent onAttack;
	public UnityEvent onUse;

	public void Attack()
	{
		onAttack.Invoke();
	}

	public void Use()
	{
		onUse.Invoke();
	}

}