using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public static bool inGame;
    public GameObject ready;
    public Text timeText;

    float time = 100;
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
        if (BallManager.bm.balls.Count == 0 && HexagonManager.hm.hexagons.Count == 0)
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

    public IEnumerator GameStart()
    {
        yield return new WaitForSeconds(3);
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
