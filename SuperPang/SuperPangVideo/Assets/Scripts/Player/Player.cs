using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    bool rightWall;
    bool leftWall;
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
        if (leftWall)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
                speedX = 0;
            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
                speedX = 4;
        }

        if (rightWall)
        {
            if (Input.GetKey(KeyCode.RightArrow))
                speedX = 0;
            else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
                speedX = 4;
        }

        rb.MovePosition(rb.position + Vector2.right * movementX * Time.fixedDeltaTime);
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Left")
            leftWall = true;
        else if (other.gameObject.tag == "Right")
            rightWall = true;
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Left")
            leftWall = false;
        else if (other.gameObject.tag == "Right")
            rightWall = false;
    }
}
