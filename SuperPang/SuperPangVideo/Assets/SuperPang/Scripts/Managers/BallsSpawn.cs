using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsSpawn : MonoBehaviour
{
    public static BallsSpawn bs;
    public bool free;
    public GameObject[] ballsPrefabs;
    public GameObject[] hexagonsPrefabs;

    GameObject ball = null;
    GameObject hexagon = null;

    void Awake()
    {
        if (bs == null)
            bs = this;
        else if (bs != null)
            Destroy(gameObject);
    }
	
	void Update ()
    {
        if (ball != null && ball.transform.position.y <= 4.4f && !free)
        {
            free = true;
            ball.GetComponent<Ball>().StartForce(ball);
        }
	}

    public void NewBall()
    {
        if (!FreezeManager.fm.freeze)
        {
            ball = Instantiate(ballsPrefabs[Random.Range(0, ballsPrefabs.Length)], new Vector2(AleatoryPosition(), transform.position.y), Quaternion.identity);
            BallManager.bm.balls.Add(ball);
            StartCoroutine(MoveDown());
        }
    }

    float AleatoryPosition()
    {
        return (Random.Range(-7.25f, 7.25f));
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            free = false;
        }
    }

    IEnumerator MoveDown()
    {
        yield return new WaitForSeconds(1);

        while (!free)
        {
            ball.transform.position = new Vector2(ball.transform.position.x, ball.transform.position.y - 0.5f);
            yield return new WaitForSeconds(1);
        }
    }
}
