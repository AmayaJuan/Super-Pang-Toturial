using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public bool blink;
    public GameObject shield;

    bool rightWall;
    bool leftWall;
    float movement = 0;
    float speed = 4f;
    Animator animator;
    LifeManager lm;
    Rigidbody2D rb;
    SpriteRenderer sr;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        lm = FindObjectOfType<LifeManager>();
    }
	
	void Update ()
    {
        if (GameManager.inGame)
        {
            movement = Input.GetAxisRaw("Horizontal") * speed;
            animator.SetInteger("VelX", Mathf.RoundToInt(movement));

            if (movement < 0)
                sr.flipX = true;
            else
                sr.flipX = false;
        }
	}

    void FixedUpdate()
    {
        if (GameManager.inGame)
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
    }

    public void Win()
    {
        shield.SetActive(false);
        animator.SetBool("Win", true);
    }

    void OnBecameInvisible()
    {
        Invoke("ReloadLevel", .5f);
    }

    void ReloadLevel()
    {
        lm.SubstracLifes();
        lm.RestartLifesDoll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.inGame && !FreezeManager.fm.freeze)
        {
            if (collision.gameObject.tag == "Ball" || collision.gameObject.tag == "Hexagon")
            {
                if (shield.activeInHierarchy)
                {
                    shield.SetActive(false);
                    StartCoroutine(Blinking());
                }
                else
                {
                    if (!blink)
                        StartCoroutine(Lose());
                }
            }
        }

        if (!GameManager.inGame && (collision.gameObject.tag == "Right" || collision.gameObject.tag == "Left"))
        {
            sr.flipX = !sr.flipX;
            rb.velocity /= 3;
            rb.velocity *= -1;
            rb.AddForce(Vector3.up * 5, ForceMode2D.Impulse);
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
            if (blink && GameManager.inGame)
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

    IEnumerator Lose()
    {
        GameManager.inGame = false;
        animator.SetBool("Lose", true);
        BallManager.bm.LoseGame();
        HexagonManager.hm.LoseGame();
        lm.LifeLose();
        yield return new WaitForSeconds(1);
        rb.isKinematic = false;

        if (transform.position.x < 0)
            rb.AddForce(new Vector2(-10, 10), ForceMode2D.Impulse);
        else
            rb.AddForce(new Vector2(10, 10), ForceMode2D.Impulse);
    }
}
