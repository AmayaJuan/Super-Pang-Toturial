using UnityEngine;

public class Ball : MonoBehaviour
{
    public bool right;
    public GameObject nextBall;
    public GameObject powerUp;

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
            InstaciatePrize();

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

        int score = Random.Range(100, 301);
        PopUpManager.pop.InstanciatePopUpText(gameObject.transform.position, score);
        ScoreManager.sm.UpdateScore(score);
    }

    public void StartForce(GameObject ball)
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
            if (item != null)
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

    public void SlowBall()
    {
        rb.velocity /= 1.4f; // rb.velocity = rb.velovity / 4
        rb.gravityScale = .5f;
    }

    public void NormalSpeedBall()
    {
        if (rb.velocity.x < 0)
            rb.velocity = new Vector2(-2, rb.velocity.y);
        else
            rb.velocity = new Vector2(2, rb.velocity.y);
        rb.gravityScale = 1f;
    }

    void InstaciatePrize()
    {
        int aleatory = GameManager.gm.AleatoryNumber();

        if (aleatory == 1)
            Instantiate(powerUp, transform.position, Quaternion.identity);
    }
}
