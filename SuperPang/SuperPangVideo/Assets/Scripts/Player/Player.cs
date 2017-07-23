using UnityEngine;

public class Player : MonoBehaviour
{
    bool rightWall;
    bool leftWall;
    float speed = 4f;
    float movement = 0;
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
        animator.SetInteger("VelX", Mathf.RoundToInt(movement));
        if (movement < 0)
            sr.flipX = true;
        else
            sr.flipX = false;
	}

    void FixedUpdate()
    {
        if(leftWall)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
                speed = 0;
            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
                speed = 4f;
        }

        if (rightWall)
        {
            if (Input.GetKey(KeyCode.RightArrow))
                speed = 0;
            else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
                speed = 4f;
        }

        rb.MovePosition(rb.position + Vector2.right * movement * Time.fixedDeltaTime);
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
