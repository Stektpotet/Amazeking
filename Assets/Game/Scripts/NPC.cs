using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
public class NPC : MonoBehaviour
{
	Animator anim;
	Rigidbody2D body;

	bool alive = true;

	public UnityEvent onDie;

	void Awake()
	{
		anim = GetComponent<Animator>();
		body = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		anim.SetBool("alive", alive);
		anim.SetFloat("yVelocity", body.velocity.y);
	}
	
	public void StopMove()
	{
		body.velocity = Vector2.zero;
	}

	public void PlayAnimation(string animationName)
	{
		anim.Play(animationName);
	}

	public void Die()
	{
		alive = false;
		onDie.Invoke();
	}
}
