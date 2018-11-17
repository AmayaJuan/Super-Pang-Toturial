using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameObject shield;
    [HideInInspector]
    public bool blink;

    bool rightWall;
    bool leftWall;
    float speed = 4f;
    float movement = 0;
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer sr;
    LifeManager lm;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        lm = FindObjectOfType<LifeManager>();
    }

    void Update()
    {
        if (GameManager.inGame)
        {
            movement = Input.GetAxisRaw("Horizontal") * speed;
            animator.SetInteger("velX", Mathf.RoundToInt(movement));

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
                else if (Input.GetKey(KeyCode.RightArrow))
                    speed = 4;
            }

            if (rightWall)
            {
                if (Input.GetKey(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
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
        animator.SetBool("win", true);
    }

    void OnBecameInvisible()
    {
        Invoke("ReloadLevel", 0.5f);
    }

    void ReloadLevel()
    {
        lm.SubstractLifes();
        lm.RestartLifesDoll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(GameManager.inGame && !FreezeManager.fm.freeze)
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

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Left")
            leftWall = true;
        else if (collision.gameObject.tag == "Right")
            rightWall = true;
    }

    void OnTriggerExit2D(Collider2D collision)
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
                yield return new WaitForSeconds(0.2f);
                sr.color = new Color(1, 1, 1, 1);
                yield return new WaitForSeconds(0.2f);
            }
            else
                break;
        }
        blink = false;
    }

    IEnumerator Lose()
    {
        GameManager.inGame = false;
        animator.SetBool("lose", true);
        BallManager.bm.LoseGame();
        HexagonManager.hm.LoseGame();
        lm.LifesLose();

        yield return new WaitForSeconds(1f);
        rb.isKinematic = false;

        if (transform.position.x < 0)
            rb.AddForce(new Vector2(-10, 10), ForceMode2D.Impulse);
        else
            rb.AddForce(new Vector2(10, 10), ForceMode2D.Impulse);
    }
}
