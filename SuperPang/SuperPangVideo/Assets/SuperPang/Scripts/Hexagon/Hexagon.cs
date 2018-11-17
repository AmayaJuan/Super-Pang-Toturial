using UnityEngine;

public class Hexagon : MonoBehaviour
{
    public GameObject nextHexagon;
    public GameObject poweUp;
    [HideInInspector]
    public bool right;
    [HideInInspector]
    public float forceX = 5f;
    [HideInInspector]
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

                hex2.GetComponent<Hexagon>().forceX = -forceX;
                hex2.GetComponent<Hexagon>().forceY = forceY;
            }
            else
            {
                hex1.GetComponent<Hexagon>().currentForceX = forceX;
                hex1.GetComponent<Hexagon>().currentForceY = forceY;

                hex2.GetComponent<Hexagon>().currentForceX = -forceX;
                hex2.GetComponent<Hexagon>().currentForceY = forceY;
            }

            if (!HexagonManager.hm.spliting)
                HexagonManager.hm.DestroyHexagon(gameObject, hex1, hex2);
        }
        else
            HexagonManager.hm.LastHexagon(gameObject);

        int score = Random.Range(100, 301);
        PopUpManager.pm.IntanciatePopUpText(gameObject.transform.position, score);
        ScoreManager.sm.UpdateScore(score);
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
                item.GetComponent<Hexagon>().forceX = 0;
                item.GetComponent<Hexagon>().forceY = 0;
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
        if (rb.velocity.x < 0)
            forceX = -3;
        else
            forceX = 3;

        if (rb.velocity.y < 0)
            forceY = -3;
        else
            forceY = 3;
    }

    public void NormalSpeedHexagon()
    {
        if (rb.velocity.x < 0)
            forceX = -5;
        else
            forceX = 5;

        if (rb.velocity.y < 0)
            forceY = -5;
        else
            forceY = 5;
    }

    void InstanciatePrize()
    {
        int aleatory = GameManager.gm.AleatoryNumber();

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

