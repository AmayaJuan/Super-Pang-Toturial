using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public static bool inGame;
    public Text timeText; 
    public GameObject ready;

    float time = 100f;
    Player player;

    void Awake()
    {
        if (gm == null)
            gm = this;
        else if (gm != this)
            Destroy(gameObject);

        player = FindObjectOfType<Player>();
    }

    void Start ()
    {
        StartCoroutine(GameStart());
	}
	
	void Update ()
    {
        if (HexagonManager.hm.hexagons.Count == 0 && BallManager.bm.balls.Count == 0)
        {
            inGame = false;
            player.Win();
        }

        if (inGame)
        {
            time -= Time.deltaTime;
            timeText.text = "TIME " + time.ToString("f0");
        }
    }

    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(2);
        ready.SetActive(false);
        BallManager.bm.StartGame();
        HexagonManager.hm.StartGame();
        inGame = true;
    }

    public int AleatoryNumber()
    {
        return Random.Range(0, 3);
    }
}
