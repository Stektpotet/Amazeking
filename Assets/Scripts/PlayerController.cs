using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerController : MonoBehaviour
{
	//Properties
	bool facingRight = true;
	public bool grounded = false;
	public float maxSpeed;
	public float jumpForce;

	//Components
	Rigidbody2D body;
	Animator anim;

	//Other
	public LayerMask groundMask;
	public Transform groundPoint;
	public float groundRadius = 0.2f;

	void Start()
	{
		body = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}


	void Update()
	{
		Movement();
	}

	void FixedUpdate()
	{
		FixedMovement();
	}

	void Movement()
	{
		
		if(Input.GetKeyDown(KeyCode.Space) && grounded)
		{
			body.AddForce(Vector2.up * jumpForce);
		}
	}

	void FixedMovement()
	{
		grounded = Physics2D.OverlapCircle(groundPoint.position, groundRadius, groundMask);

		float move = Input.GetAxis("Horizontal");

		anim.SetFloat("Speed", Mathf.Abs(move));

		body.velocity = new Vector2(move * maxSpeed, body.velocity.y);

		if(move < 0 && facingRight || move > 0 && !facingRight)
		{
			Flip();
		}
	}
    void Flip()
	{
		facingRight = !facingRight;
		GetComponent<SpriteRenderer>().flipX = !facingRight;
	}
}
