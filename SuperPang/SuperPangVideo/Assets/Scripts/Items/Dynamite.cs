using UnityEngine;

public class Dynamite : MonoBehaviour
{
    bool inGround;

    void Update()
    {
        if (!inGround)
            transform.position += Vector3.down * Time.deltaTime * 2;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            inGround = true;
            Destroy(gameObject, 15);
        }
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            BallManager.bm.Dynamite(5);
            HexagonManager.hm.Dynamite(4);
        }
    }
}
