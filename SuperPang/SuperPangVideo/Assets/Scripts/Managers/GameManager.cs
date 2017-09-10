using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public static bool inGame;
    public GameObject ready;

    void Awake()
    {
        if (gm == null)
            gm = this;
        else if (gm != this)
            Destroy(gameObject);
    }

    void Start ()
    {
        StartCoroutine(GameStart());
	}
	
	void Update ()
    {
		
	}

    public IEnumerator GameStart()
    {
        yield return new WaitForSeconds(3);
        ready.SetActive(false);
        BallManager.bm.StartGame();
        inGame = true;
    }
}
