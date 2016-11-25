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

	public float idleTime = 0;

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

		anim.SetBool("Grounded", grounded);
		anim.SetFloat("Speed", Mathf.Abs(body.velocity.x));
		anim.SetFloat("VelocityY", body.velocity.y);

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
	}

}