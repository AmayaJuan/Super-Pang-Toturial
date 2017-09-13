using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public bool blink;
    public bool climb;
    public bool inGround;
    public bool isUp;
    public GameObject shield;

    bool rightWall;
    bool leftWall;
    float defaultPosY;
    float maxClimbY = 0;
    float movementX = 0;
    float movementY = 0;
    float newY;
    float speedX = 4f;
    float speedY = 4f;
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

    void Start()
    {
        defaultPosY = transform.position.y;
    }

    void Update ()
    {
        if (GameManager.inGame)
        {
            movementX = Input.GetAxisRaw("Horizontal") * speedX;
            movementY = Input.GetAxisRaw("Vertical") * speedY;
            animator.SetInteger("VelX", Mathf.RoundToInt(movementX));
            animator.SetInteger("VelY", Mathf.RoundToInt(movementY));

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

            if (transform.position.y >= maxClimbY)
                isUp = true;
            else
                isUp = false;

            if (climb)
            {
                if ((Input.GetKey(KeyCode.UpArrow) && !isUp) || (Input.GetKey(KeyCode.DownArrow)  && !inGround))
                    speedY = 4;
                else
                    speedY = 0;
            }
            else
                speedY = 0;
        }

        if (movementX != 0)
            rb.MovePosition(rb.position + Vector2.right * movementX * Time.fixedDeltaTime);
        else if ((transform.position.y >= defaultPosY && climb && !isUp) ||(isUp && Input.GetKey(KeyCode.DownArrow)))
            rb.MovePosition(rb.position + Vector2.up * movementY * Time.fixedDeltaTime);

        newY = Mathf.Clamp(transform.position.y, -2.5f, 8);
        transform.position = new Vector2(transform.position.x, newY);

        if (!inGround && !climb)
            transform.position += new Vector3(movementX / 5, -1) * Time.deltaTime * 3;
    }

    public void Win()
    {
        shield.SetActive(false);
        animator.SetBool("Win", true);
    }

    void OnBecameInvisible()
    {
        if (lm.lifes <= 0)    
            GameManager.gm.StartGameOver();
        else
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

            if (collision.gameObject.tag == "Ladder")
            {
                if (!isUp)
                    maxClimbY = transform.position.y + collision.GetComponent<BoxCollider2D>().size.y -.2f;
            }

            if (collision.gameObject.tag == "Platform" && transform.position.y < collision.gameObject.transform.position.y && inGround)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + .4f);
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

        if (collision.gameObject.tag == "Ladder")
            climb = true;

        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Platform" && transform.position.y > collision.gameObject.transform.position.y)
            inGround = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
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
