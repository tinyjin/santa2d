using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantaObject : MonoBehaviour {
	public float speed;
	
	private Vector3 vector;
	private Animator animator;
	
	// Use this for initialization
	void Start ()
	{
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
		{
			vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

			if (vector.x != 0)
			{
				vector.y = 0;
			}
			
			animator.SetFloat("DirX", vector.x);
			animator.SetFloat("DirY", vector.y);
			
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
		}
		else
		{
			animator.SetBool("Walking", false);
			animator.SetBool("Standing", true);
		}
	}
}
