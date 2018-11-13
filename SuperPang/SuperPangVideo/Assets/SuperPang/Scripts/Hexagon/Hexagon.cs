using UnityEngine;

public class Hexagon : MonoBehaviour
{
    public GameObject nextHexagon;
    public GameObject poweUp;
    public bool right;
    public float forceX = 5f;
    public float forceY = 5f;

    float currentForceX;
    float currentForceY;
    float rotSpeed;
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (GameManager.inGame)
        {
            if (!FreezeManager.fm.freeze)
            {
                rotSpeed = 250 * Time.deltaTime;
                transform.Rotate(0, 0, rotSpeed);
                rb.velocity = new Vector2(forceX, forceY);
            }
        }
    }

    public void Split()
    {
        if (nextHexagon != null)
        {
            InstanciatePrize();
            GameObject hex1 = Instantiate(nextHexagon, rb.position + Vector2.right / 4, Quaternion.identity);
            hex1.GetComponent<Hexagon>().right = true;
            GameObject hex2 = Instantiate(nextHexagon, rb.position + Vector2.left / 4, Quaternion.identity);
            hex2.GetComponent<Hexagon>().right = false;

            if (!FreezeManager.fm.freeze)
            {
                hex1.GetComponent<Hexagon>().forceX = forceX;
                hex1.GetComponent<Hexagon>().forceY = forceY;

                hex2.GetComponent<Hexagon>().forceX = forceX;
                hex2.GetComponent<Hexagon>().forceY = forceY;
            }
            else
            {
                hex1.GetComponent<Hexagon>().currentForceX = forceX;
                hex1.GetComponent<Hexagon>().currentForceY = forceY;

                hex2.GetComponent<Hexagon>().currentForceX = forceX;
                hex2.GetComponent<Hexagon>().currentForceY = forceY;
            }

            if (!HexagonManager.hm.spliting)
                HexagonManager.hm.DestroyHexagon(gameObject, hex1, hex2);
        }
        else
            HexagonManager.hm.LastHexagon(gameObject);
    }

    public void StartForce(GameObject hex)
    {
        if (right)
            hex.GetComponent<Hexagon>().forceX = forceX;
        else
            hex.GetComponent<Hexagon>().forceX = -forceX;

        hex.GetComponent<Hexagon>().forceY = forceY;
    }

    public void FreezeHexagon(params GameObject[] hexagons)
    {
        foreach (GameObject item in hexagons)
        {
            if (item != null)
            {
                currentForceX = item.GetComponent<Hexagon>().forceX;
                currentForceY = item.GetComponent<Hexagon>().forceY;
                item.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }
    }

    public void UnFreezeBall(params GameObject[] hexagons)
    {
        foreach (GameObject item in hexagons)
        {
            if (item != null)
            {
                item.GetComponent<Hexagon>().forceX = currentForceX;
                item.GetComponent<Hexagon>().forceY = currentForceY;
            }
        }
    }

    public void SlowHexagon()
    {
        rb.velocity /= 1.4f; // rb.velocity = rb.velocity / 4
        rb.gravityScale = 0.5f;
    }

    public void NormalSpeedHexagon()
    {
        if (rb.velocity.x < 0)
            rb.velocity = new Vector2(-forceX, forceY);
        else
            rb.velocity = new Vector2(forceX, forceY);

        rb.gravityScale = 1f;
    }

    void InstanciatePrize()
    {
        int aleatory = BallManager.bm.AleatoryNumber();

        if (aleatory == 1)
            Instantiate(poweUp, transform.position, Quaternion.identity);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Roof")
            forceY *= -1;
        if (collision.gameObject.tag == "Left" || collision.gameObject.tag == "Right")
            forceX *= -1;

    }
}

