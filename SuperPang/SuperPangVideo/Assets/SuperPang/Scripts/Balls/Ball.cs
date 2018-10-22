using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public GameObject nextBall;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Split()
    {
        if (nextBall != null)
        {
            GameObject ball1 = Instantiate(nextBall, rb.position + Vector2.right / 4, Quaternion.identity);
            GameObject ball2 = Instantiate(nextBall, rb.position + Vector2.left / 4, Quaternion.identity);

            ball1.GetComponent<Rigidbody2D>().AddForce(new Vector2(2, 5), ForceMode2D.Impulse);
            ball2.GetComponent<Rigidbody2D>().AddForce(new Vector2(-2, 5), ForceMode2D.Impulse);

            BallManager.bm.DestroyBall(gameObject, ball1, ball2);
        }
    }
}
