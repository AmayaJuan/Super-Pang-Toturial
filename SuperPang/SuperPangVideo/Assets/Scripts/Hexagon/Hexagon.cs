using UnityEngine;

public class Hexagon : MonoBehaviour
{
    public bool right;
    public GameObject nextHexagon;
    public GameObject powerUp;

    float currentForceX;
    float currentForceY;
    float forceX = 5;
    float forceY = 5;
    float rotSpeed;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (GameManager.inGame)
        {
            if (!FreezeManager.fm.freeze)
            {
                rotSpeed = Time.deltaTime * 250;
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

    public void Startforce(GameObject hex)
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

    public void UnFreezeHexagon(params GameObject[] hexagons)
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
        rb.velocity /= 1.4f;
        rb.gravityScale = .5f;
    }

    public void NormalSpeedHexagon()
    {
        if (rb.velocity.x < 0)
            rb.velocity = new Vector2(-forceX, forceY);
        else
            rb.velocity = new Vector2(forceX, forceY);

        rb.gravityScale = 1;
    }

    void InstanciatePrize()
    {
        int aleatory = HexagonManager.hm.AleatoryNumber();

        if (aleatory == 1)
            Instantiate(powerUp, transform.position, Quaternion.identity);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Roof")
            forceY *= 1;

        if (other.gameObject.tag == "Left" || other.gameObject.tag == "Right")
            forceX *= 1;
    }
}
