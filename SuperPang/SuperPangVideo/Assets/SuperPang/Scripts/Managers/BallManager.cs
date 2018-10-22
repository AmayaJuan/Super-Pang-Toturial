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
	}
	
	void Update ()
    {
		
	}

    public void DestroyBall(GameObject ball, GameObject ball1, GameObject ball2)
    {
        Destroy(ball);
        balls.Remove(ball);
        balls.Add(ball1);
        balls.Add(ball2);
    }
}
