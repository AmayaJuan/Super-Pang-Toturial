using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float movementX = 0;
    float speedX = 4.0f;
    Animator animator;
    SpriteRenderer sr;
    Rigidbody2D rb;
    
    void Awake()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start ()
    {
		
	}
	
	void Update ()
    {
        movementX = Input.GetAxisRaw("Horizontal") * speedX;
        animator.SetInteger("VelX", Mathf.RoundToInt(movementX));

        if (movementX < 0)
            sr.flipX = true;
        else
            sr.flipX = false;
	}

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + Vector2.right * movementX * Time.fixedDeltaTime);
    }
}
