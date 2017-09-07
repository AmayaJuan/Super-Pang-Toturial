using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float movement = 0;
    float speed = 4f;
    Animator animator;
    Rigidbody2D rb;
    SpriteRenderer sr;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Start ()
    {
		
	}
	
	void Update ()
    {
        movement = Input.GetAxisRaw("Horizontal") * speed;
        animator.SetInteger("VelX", Mathf.RoundToInt(movement));

        if (movement < 0)
            sr.flipX = true;
        else
            sr.flipX = false;
	}

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + Vector2.right * movement * Time.fixedDeltaTime);
    }
}
