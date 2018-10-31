using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public GameObject nextBall;
    public bool right;

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
            ball1.GetComponent<Ball>().right = true;
            ball2.GetComponent<Rigidbody2D>().AddForce(new Vector2(-2, 5), ForceMode2D.Impulse);
            ball2.GetComponent<Ball>().right = false;

            BallManager.bm.DestroyBall(gameObject, ball1, ball2);
        }
        else
            BallManager.bm.LastBall(gameObject);
    }

    public void StartForce(GameObject ball)
    {
        if (right)
            ball.GetComponent<Rigidbody2D>().AddForce(Vector3.right * 2, ForceMode2D.Impulse);
        else
            ball.GetComponent<Rigidbody2D>().AddForce(Vector3.left * 2, ForceMode2D.Impulse);
    }
}
