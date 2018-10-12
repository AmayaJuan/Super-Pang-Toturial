using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    readonly float speed = 4f;
    float movement = 0;
    float newX;
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer sr;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

	void Update ()
    {
        movement = Input.GetAxisRaw("Horizontal") * speed;
        animator.SetInteger("velX", Mathf.RoundToInt(movement));

        if (movement < 0)
            sr.flipX = true;
        else
            sr.flipX = false;
	}

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + Vector2.right * movement * Time.fixedDeltaTime);
        newX = Mathf.Clamp(transform.position.x, -8, 8);
        transform.position = new Vector2(newX, transform.position.y);
    }
}
