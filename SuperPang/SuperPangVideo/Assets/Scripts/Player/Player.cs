using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool blink;
    public GameObject shield;

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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            if (shield.activeInHierarchy)
            {
                shield.SetActive(false);
                StartCoroutine(Blinking());
            }
            else
            {
                if (!blink)
                {
                    //FINALIZAR PARTIDA
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Left")
            leftWall = true;
        else if (collision.gameObject.tag == "Right")
            rightWall = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Left")
            leftWall = false;
        else if (collision.gameObject.tag == "Right")
            rightWall = false;
    }

    public IEnumerator Blinking()
    {
        blink = true;

        for (int i = 0; i < 8; i++)
        {
            if (blink)
            {
                sr.color = new Color(1, 1, 1, 0);
                yield return new WaitForSeconds(.2f);
                sr.color = new Color(1, 1, 1, 1);
                yield return new WaitForSeconds(.2f);
            }
            else
                break;
        }

        blink = false;
    }
}
