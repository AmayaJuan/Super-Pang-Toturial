using UnityEngine;

public class Player : MonoBehaviour
{
    bool rightWall;
    bool leftWall;
    float movement = 0;
    float newX;
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
        if (leftWall)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
                speed = 0;
            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
                speed = 4;
        }

        if (rightWall)
        {
            if (Input.GetKey(KeyCode.RightArrow))
                speed = 0;
            else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
                speed = 4;
        }

        rb.MovePosition(rb.position + Vector2.right * movement * Time.fixedDeltaTime);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Left")
            leftWall = true;
        else if (collision.gameObject.tag == "Right")
            rightWall = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Left")
            leftWall = false;
        else if (collision.gameObject.tag == "Right")
            rightWall = false;
    }
}
