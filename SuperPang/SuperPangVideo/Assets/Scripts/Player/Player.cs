using UnityEngine;

public class Player : MonoBehaviour
{
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
