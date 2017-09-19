using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public bool right;
    public GameObject nextBall;

    Vector2 currentVelocity;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Split()
    {
        if (nextBall != null)
        {
            GameObject ball1 = Instantiate(nextBall, rb.position + Vector2.right / 4, Quaternion.identity);
            ball1.GetComponent<Ball>().right = true;
            GameObject ball2 = Instantiate(nextBall, rb.position + Vector2.left / 4, Quaternion.identity);
            ball2.GetComponent<Ball>().right = false;

            if (!FreezeManager.fm.freeze)
            {
                ball1.GetComponent<Rigidbody2D>().isKinematic = false;
                ball1.GetComponent<Rigidbody2D>().AddForce(new Vector2(2, 5), ForceMode2D.Impulse);  
                ball2.GetComponent<Rigidbody2D>().isKinematic = false;
                ball2.GetComponent<Rigidbody2D>().AddForce(new Vector2(-2, 5), ForceMode2D.Impulse);
            }
            else
            {
                ball1.GetComponent<Ball>().currentVelocity = new Vector2(2, 5);
                ball2.GetComponent<Ball>().currentVelocity = new Vector2(-2, 5);
            }
            if (!BallManager.bm.spliting)
                BallManager.bm.DestroyBall(gameObject, ball1, ball2);
        }
        else
            BallManager.bm.LastBall(gameObject);
    }

    public void Startforce(GameObject ball)
    {
        ball.GetComponent<Rigidbody2D>().isKinematic = false;

        if (right)
            ball.GetComponent<Rigidbody2D>().AddForce(Vector3.right * 2, ForceMode2D.Impulse);
        else
            ball.GetComponent<Rigidbody2D>().AddForce(Vector3.left * 2, ForceMode2D.Impulse);
    }

    public void FreezeBall(params GameObject[] balls)
    {
        foreach (GameObject item in balls)
        {
            if (item != null )
            {
                currentVelocity = item.GetComponent<Rigidbody2D>().velocity;
                item.GetComponent<Rigidbody2D>().isKinematic = true;
                item.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }
    }

    public void UnFreezeBall(params GameObject[] balls)
    {
        foreach (GameObject item in balls)
        {
            if (item != null)
            {
                item.GetComponent<Rigidbody2D>().isKinematic = false;
                item.GetComponent<Rigidbody2D>().AddForce(currentVelocity, ForceMode2D.Impulse);
            }
        }
    }
}
