using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyBandit : MonoBehaviour
{
	public HpController hp;

	public static HeavyBandit instance;

	public GamePlayController GamePlayController;

	private Animator animator;

	public States states;

	public float dmg;

	public float maxHp; 

	public float speed;

	public bool isDie;

	public TypePlayer type;

	[SerializeField] GameObject Attack;

	void Start()
	{
		states = States.idel;
		animator = GetComponent<Animator>();
		maxHp = hp.maxHp;
		isDie = false;
	}

	// Update is called once per frame
	void Update()
	{
		if (type == TypePlayer.Blue && isDie == false)
		{
			ControllBlue();
		}
		else if (type == TypePlayer.Red && isDie == false)
		{
			ControllRed();
		}
		SetStatesPlayer();
		if(hp.hpcurrent == 0)
		{
			isDie = true;
			animator.SetTrigger("Death");
		}
	}
	public void ControllBlue()
	{
		if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
		{
			states = States.idel;
		}

		// Swap direction of sprite depending on walk direction
		if (Input.GetKeyDown(KeyCode.D))
		{
			states = States.right;
			transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
		}
		else if (Input.GetKeyDown(KeyCode.A))
		{
			states = States.left;
			transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
		}


		if (Input.GetKeyDown("space"))
		{

			animator.SetTrigger("Attack");
			StartCoroutine(attack());

		}
	}

	public void ControllRed()
	{
		if (Input.GetKeyUp(KeyCode.H) || Input.GetKeyUp(KeyCode.K))
		{
			states = States.idel;
		}

		// Swap direction of sprite depending on walk direction
		if (Input.GetKeyDown(KeyCode.K))
		{
			states = States.right;
			transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
		}
		else if (Input.GetKeyDown(KeyCode.H))
		{
			states = States.left;
			transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
		}


		if (Input.GetKeyDown(KeyCode.J)) 
		{

			animator.SetTrigger("Attack");
			StartCoroutine(attack());

		}
	}
	public void CheckCollider()
	{
		if( type == TypePlayer.Red)
		{
			GamePlayController.CheckCollider(TypePlayer.Red ,(int)dmg);
		}
		else
		{
			GamePlayController.CheckCollider(TypePlayer.Blue,(int)dmg);
		}
	}
	public void SetStatesPlayer()
	{
		switch (states)
		{
			case States.idel:
				animator.SetInteger("AnimState", 0);
				break;
			case States.left:
				gameObject.transform.localPosition -= new Vector3(0.01f, 0, 0);
				animator.SetInteger("AnimState", 2);
				break;
			case States.right:
				gameObject.transform.localPosition += new Vector3(0.01f, 0, 0);
				animator.SetInteger("AnimState", 2);
				break;
		}
	}
	IEnumerator attack()
	{
		yield return new WaitForSeconds(0.5f);
		Attack.SetActive(true);
		CheckCollider();
		yield return new WaitForSeconds(0.02f);
		Attack.SetActive(false);
	}
	
}
public enum TypePlayer { Red, Blue};
public enum States { left , right, up , die , idel };