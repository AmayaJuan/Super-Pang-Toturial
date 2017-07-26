using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public static BallManager bm;
    public List<GameObject> balls = new List<GameObject>();

    void Awake()
    {
        if (bm == null)
            bm = this;
        else if (bm != this)
            Destroy(gameObject);
    }

    void Start ()
    {
        balls.AddRange(GameObject.FindGameObjectsWithTag("Ball"));
        StartGame();
	}
	
	void Update ()
    {
		
	}

    public void StartGame()
    {
        foreach (GameObject item in balls)
        {
            if (balls.IndexOf(item) % 2 == 0)
                item.GetComponent<Ball>().right = true;
            else
                item.GetComponent<Ball>().right = false;

            item.GetComponent<Ball>().StartForce(item);
        }
    }

    public void DestroyBall(GameObject ball, GameObject ball1, GameObject ball2)
    {
        balls.Remove(ball);
        Destroy(ball);
        balls.Add(ball1);
        balls.Add(ball2);
    }

    public void LastBall(GameObject ball)
    {
        balls.Remove(ball);
        Destroy(ball);
    }
}
