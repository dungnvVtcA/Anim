﻿using UnityEngine;
using System.Collections;

public class GuardScript : MonoBehaviour {

    [SerializeField] float      speed = 1.0f;
    [SerializeField] float      jumpForce = 4.0f;
    [SerializeField] GameObject Attack;
    private float               inputX;
    private Animator            animator;
    private Rigidbody2D         body2d;
    private bool                combatIdle = false;
    private bool                isGrounded = true;
    private bool attacking = false;
    private float attackDelay = 0f;
	public Type type;
    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        body2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        // -- Handle input and movement --
        if(type == Type.RED)
		{
			RedControll();
		}
		if(type == Type.Blue)
		{
			BlueControll();
		}
        
    }
	public void BlueControll()
	{
		inputX = Input.GetAxis("Horizontal");

		// Swap direction of sprite depending on walk direction
		if (inputX > 0)
			transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
		else if (inputX < 0)
			transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

		// Move
		//if(Input.GetKeyDown(KeyCode.A))
		//{
		//	gameObject.transform.localPosition -= new Vector3(10f, 0, 0);
		//}
		body2d.velocity = new Vector2(inputX * speed, body2d.velocity.y);

		// -- Handle Animations --
		isGrounded = IsGrounded();
		animator.SetBool("Grounded", isGrounded);

		//Death
		if (Input.GetKeyDown("k"))
			animator.SetTrigger("Death");
		//Hurt
		else if (Input.GetKeyDown("h"))
			animator.SetTrigger("Hurt");
		//Recover
		else if (Input.GetKeyDown("r"))
			animator.SetTrigger("Recover");
		//Change between idle and combat idle
		else if (Input.GetKeyDown("i"))
			combatIdle = !combatIdle;



		//Attack
		else if (Input.GetMouseButtonDown(0))
		{
			animator.SetTrigger("Attack");
			StartCoroutine(attack());

		}

		//Jump
		else if (Input.GetKeyDown(KeyCode.P) && isGrounded)
		{
			animator.SetTrigger("Jump");
			body2d.velocity = new Vector2(body2d.velocity.x, jumpForce);

		}

		//Walk
		else if (Mathf.Abs(inputX) > Mathf.Epsilon && isGrounded)
			animator.SetInteger("AnimState", 2);
		//Combat idle
		else if (combatIdle)
			animator.SetInteger("AnimState", 1);
		//Idle
		else
			animator.SetInteger("AnimState", 0);
	}
	public void RedControll()
	{
		inputX = Input.GetAxis("Horizontal");

		// Swap direction of sprite depending on walk direction
		if (inputX > 0)
			transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
		else if (inputX < 0)
			transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

		// Move
		body2d.velocity = new Vector2(inputX * speed, body2d.velocity.y);

		// -- Handle Animations --
		isGrounded = IsGrounded();
		animator.SetBool("Grounded", isGrounded);

		//Death
		if (Input.GetKeyDown("k"))
			animator.SetTrigger("Death");
		//Hurt
		else if (Input.GetKeyDown("h"))
			animator.SetTrigger("Hurt");
		//Recover
		else if (Input.GetKeyDown("r"))
			animator.SetTrigger("Recover");
		//Change between idle and combat idle
		else if (Input.GetKeyDown("i"))
			combatIdle = !combatIdle;



		//Attack
		else if (Input.GetKeyDown("space"))
		{
			animator.SetTrigger("Attack");
			StartCoroutine(attack());

		}

		//Jump
		else if (Input.GetKeyDown(KeyCode.W) && isGrounded)
		{
			animator.SetTrigger("Jump");
			body2d.velocity = new Vector2(body2d.velocity.x, jumpForce);

		}

		//Walk
		else if (Mathf.Abs(inputX) > Mathf.Epsilon && isGrounded)
			animator.SetInteger("AnimState", 2);
		//Combat idle
		else if (combatIdle)
			animator.SetInteger("AnimState", 1);
		//Idle
		else
			animator.SetInteger("AnimState", 0);

	}
	IEnumerator attack()
    {
        yield return new WaitForSeconds(0.5f);
        Attack.SetActive(true);
        yield return new WaitForSeconds(0.02f);
        Attack.SetActive(false);
    }
    
    bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, -Vector3.up, 0.03f);
    }
	public enum Type { RED, Blue}
}
