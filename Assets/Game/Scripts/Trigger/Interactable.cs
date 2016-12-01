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

	public void AddForceUp(float force)
	{
		GetComponent<Rigidbody2D>().AddForce(force*Vector2.up);
	}

	public void AddForceRight(float force)
	{
		GetComponent<Rigidbody2D>().AddForce(force * Vector2.right);
	}
}