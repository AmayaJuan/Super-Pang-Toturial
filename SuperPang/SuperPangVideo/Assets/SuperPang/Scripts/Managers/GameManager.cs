using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public static bool inGame;
    public GameObject ready;
    public GameObject panel;
    [HideInInspector]
    public float time = 100;
    [HideInInspector]
    public int ballsDestroy = 0;
    [HideInInspector]
    public int fruitsCached = 0;
    public Text timeText;
    [HideInInspector]
    public PanelPoints panelPoints;

    Fruits fruits;
    LifeManager lm;
    Player player;
    
    void Awake()
    {
        if (gm == null)
            gm = this;
        else if (gm != this)
            Destroy(gameObject);

        player = FindObjectOfType<Player>();
        lm = FindObjectOfType<LifeManager>();
        fruits = FindObjectOfType<Fruits>();
    }

    void Start ()
    {
        StartCoroutine(GameStart());
        ScoreManager.sm.UpdateHiScore();
	}
	
	void Update ()
    {
		if(BallManager.bm.balls.Count == 0 && HexagonManager.hm.hexagons.Count == 0)
        {
            inGame = false;
            player.Win();
            lm.LifesWin();
            panel.SetActive(true);
            panelPoints = panel.GetComponent<PanelPoints>();
        }

        if (inGame)
        {
            time -= Time.deltaTime;
            timeText.text = "TIME: " + time.ToString("f0");
        }

	}

    public void UpdateBallsDestroyed()
    {
        ballsDestroy++;

        if (ballsDestroy % Random.Range(5, 20) == 0 && BallManager.bm.balls.Count > 0)
        {
            fruits.InstaciateFruit();
        }
    }

    public void NextLevel()
    {
        lm.RestartLifesDoll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public IEnumerator GameStart()
    {
        yield return new WaitForSeconds(2f);
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
