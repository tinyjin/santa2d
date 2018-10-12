using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantaObject : MonoBehaviour {
	public float speed;
	public int walkCount;
	private int currentWalkCount;
	private bool canMove = true;

	private Vector3 vector;
	private Animator animator;

	private BoxCollider2D boxColider;
	public LayerMask layerMask;
	
	// Use this for initialization
	void Start ()
	{
		boxColider = GetComponent<BoxCollider2D>();
		animator = GetComponent<Animator>();
	}

	IEnumerator MoveCoroutine()
	{
		vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

		while (currentWalkCount < walkCount)
		{
			if (vector.x != 0)
			{
				vector.y = 0;
			}
			
			animator.SetFloat("DirX", vector.x);
			animator.SetFloat("DirY", vector.y);

			RaycastHit2D hit;
			Vector2 start = transform.position;
			Vector2 end = start + new Vector2(vector.x * speed * walkCount, vector.y * speed * walkCount);

			boxColider.enabled = false;
			hit = Physics2D.Linecast(start, end, layerMask);
			boxColider.enabled = true;

			if (hit.transform != null)
				break;
			
			animator.SetBool("Walking", true);
			animator.SetBool("Standing", false);
			
			if (vector.x != 0)
			{
				transform.Translate(vector.x * speed, 0, 0);
			}
			else if (vector.y != 0)
			{
				transform.Translate(0, vector.y * speed, 0);
			}

			currentWalkCount++;
			yield return new WaitForSeconds(0.01f);
		}
		currentWalkCount = 0;

		canMove = true;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (canMove)
		{
			if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
			{
				canMove = false;
				StartCoroutine(MoveCoroutine());
			}	
			else
			{
				animator.SetBool("Walking", false);
				animator.SetBool("Standing", true);
			}
		}
	}
}
