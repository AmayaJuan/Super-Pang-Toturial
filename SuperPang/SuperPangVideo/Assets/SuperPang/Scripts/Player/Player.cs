using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameObject shield;
    [HideInInspector]
    public bool blink;
    [HideInInspector]
    public bool climb;
    [HideInInspector]
    public bool inGround;
    [HideInInspector]
    public bool isUp;

    bool rightWall;
    bool leftWall;
    float speedX = 4f;
    float speedY = 4f;
    float movementX = 0;
    float movementY = 0;
    float maxClimbY = 0;
    float newY;
    float defaultPosY;
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

    void Start()
    {
        defaultPosY = transform.position.y;
    }

    void Update()
    {
        if (GameManager.inGame)
        {
            movementX = Input.GetAxisRaw("Horizontal") * speedX;
            movementY = Input.GetAxisRaw("Vertical") * speedY;
            animator.SetInteger("velX", Mathf.RoundToInt(movementX));
            animator.SetInteger("velY", Mathf.RoundToInt(movementY));

            if (movementX < 0)
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
                    speedX = 0;
                else if (Input.GetKey(KeyCode.RightArrow))
                    speedX = 4;
            }

            if (rightWall)
            {
                if (Input.GetKey(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
                    speedX = 0;
                else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
                    speedX = 4;
            }

            if (transform.position.y >= maxClimbY)
                isUp = true;
            else
                isUp = false;

            if (climb)
            {
                if ((Input.GetKey(KeyCode.UpArrow) && !isUp) || (Input.GetKey(KeyCode.DownArrow) && !inGround))
                    speedY = 4f;
                else
                    speedY = 0f;
            }
            else
                speedY = 0f;

            if (movementX != 0)
                rb.MovePosition(rb.position + Vector2.right * movementX * Time.fixedDeltaTime);
            else if ((transform.position.y >= defaultPosY && climb && !isUp) || isUp && Input.GetKey(KeyCode.DownArrow))
                rb.MovePosition(rb.position + Vector2.up * movementY * Time.fixedDeltaTime);
        }
    }

    public void Win()
    {
        shield.SetActive(false);
        animator.SetBool("win", true);
    }

    void OnBecameInvisible()
    {
        if (lm.lifes <= 0)
            GameManager.gm.StartGameOver();
        else
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

            if (collision.gameObject.tag == "Ladder")
            {
                if (!isUp)
                {
                    maxClimbY = transform.position.y + collision.GetComponent<BoxCollider2D>().size.y - 0.2f;
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

        if (collision.gameObject.tag == "Ladder")
            climb = true;

        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Platform")
            inGround = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Left")
            leftWall = false;
        else if (collision.gameObject.tag == "Right")
            rightWall = false;

        if (collision.gameObject.tag == "Ladder")
            climb = false;

        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Platform")
            inGround = false;
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
