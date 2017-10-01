using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHexagonSpawn : MonoBehaviour
{
    public static BallHexagonSpawn bs;
    public bool free;
    public GameObject[] ballsPrefabs;
    public GameObject[] hexagonPrefabs;

    GameObject ball = null;
    int dificulty = 0;
    float timeSpawn = 2f;

    void Awake()
    {
        if (bs == null)
            bs = this;
        else if (bs != this)
            Destroy(gameObject);
    }

    void Update()
    {
        if (ball != null && ball.transform.position.y <= 4.4f && !free)
        {
            free = true;
            ball.GetComponent<Ball>().Startforce(ball);
            BallManager.bm.balls.Add(ball);
            ball.gameObject.tag = "Ball";

            if (ball.GetComponent<Ball>().sprites.Length > 0)
                ball.name = ball.GetComponent<SpriteRenderer>().name;

            ball = null;
        }
    }

    public void NewBall()
    {
        if (!FreezeManager.fm.freeze && ball == null)
        {
            ball = Instantiate(ballsPrefabs[Random.Range(0, ballsPrefabs.Length)], new Vector2(AleatoryPosition(), transform.position.y), Quaternion.identity);
            ball.gameObject.tag = "Untagged";
            StartCoroutine(MoveDown());
        }
    }

    float AleatoryPosition()
    {
        return (Random.Range(-7.25f, 7.25f));
    }

    public void IncreaseDificulty()
    {
        dificulty++;

        if (dificulty == 1)
            timeSpawn = 3f;
        else
            timeSpawn = Random.Range(3f, 8f);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Untagged")
            free = false;
    }

    public IEnumerator MoveDown()
    {
        if (ball != null)
        {
            yield return new WaitForSeconds(1);

            while (!free)
            {
                if (FreezeManager.fm.freeze)
                    break;

                ball.transform.position = new Vector2(ball.transform.position.x, ball.transform.position.y - .5f);
                yield return new WaitForSeconds(1);
            }

            yield return new WaitForSeconds(timeSpawn);

            if (free)
                NewBall();
        }
    }
}
