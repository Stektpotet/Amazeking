using UnityEngine;
using UnityEngine.Events;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerController : MonoBehaviour
{
	//Properties
	bool facingRight = true;
	public bool grounded = false;
	public float maxSpeed;
	public float jumpForce;

	public float idleTime = 0;

	bool forcedStop = false;
	bool alive = true;

	//Components
	Rigidbody2D body;
	Animator anim;

	//Stats
	public PlayerStats stats;

	//Other
	public LayerMask groundMask;
	public LayerMask interactMask;
	public Transform groundPoint;
	public Transform attackPoint;
	public float groundRadius = 0.2f, attackRadius = 0.5f;

	public UnityEvent onDie;
	public UnityEvent onAttack;

	void Start()
	{
		body = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}
	
	void Update()
	{
		anim.SetBool("Grounded", grounded);
		anim.SetFloat("Speed", Mathf.Abs(body.velocity.x));
		anim.SetFloat("VelocityY", body.velocity.y);

		if(!forcedStop)
		{ Movement(); }
	}

	public void Die()
	{
		onDie.Invoke();
	}

	public void TogglePlayerInput()
	{
		body.velocity = Vector2.up * body.velocity.y;
		forcedStop = !forcedStop;
	}

	void FixedUpdate()
	{
		if(!forcedStop)
		{ FixedMovement(); }
	}

	void Movement()
	{
		if(Input.GetKeyDown(KeyCode.E))
		{
			Kick();
		}
		if(Input.GetKeyDown(KeyCode.Q))
		{
			Punch();
		}
		if(Input.GetKeyDown(KeyCode.Space) && grounded)
		{
			body.AddForce(Vector2.up * jumpForce);
			stats.data.JumpCount++;
		}
	}

	void FixedMovement()
	{
		grounded = Physics2D.OverlapCircle(groundPoint.position, groundRadius, groundMask);

		float move = Input.GetAxis("Horizontal");

		body.velocity = new Vector2(move * maxSpeed, body.velocity.y);

		if(move < 0 && facingRight || move > 0 && !facingRight)
		{
			Flip();
		}
	}

	//flip the sprite
    void Flip()
	{
		facingRight = !facingRight;
		GetComponent<SpriteRenderer>().flipX = !facingRight;
		attackPoint.localPosition = new Vector3(-attackPoint.localPosition.x, attackPoint.localPosition.y);
	}

	public void Kick()
	{
		anim.Play("kick");
	}

	public void Punch()
	{
		anim.Play("punch");
    }

	public void Attack()
	{
		onAttack.Invoke();
		Collider2D col = Physics2D.OverlapPoint(attackPoint.position, interactMask);
		
		if(col != null)
		{
			col.GetComponent<Interactable>().attackFromRight = !facingRight;
			col.GetComponent<Interactable>().Attack();
		}
	}
}