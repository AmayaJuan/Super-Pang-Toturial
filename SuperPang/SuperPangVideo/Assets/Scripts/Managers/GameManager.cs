using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public static bool inGame;
    public GameObject panel;
    public GameObject ready;
    public Text timeText;
    public int fruitsCatched = 0;
    public int ballsDestroyed = 0;
    public float time = 100;

    Fruits fruits;
    LifeManager lm;
    PanelPoints panelPoints;
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
	}
	
	void Update ()
    {
        if (BallManager.bm.balls.Count == 0 && HexagonManager.hm.hexagons.Count == 0)
        {
            inGame = false;
            player.Win();
            lm.LifeWin();
            panel.SetActive(true);
            panelPoints = panel.GetComponent<PanelPoints>();
        }

        if (inGame)
        {
            time -= Time.deltaTime;
            timeText.text = "TIME " + time.ToString("f0");
        }
	}

    public void UpdateBallsDestroyed()
    {
        ballsDestroyed++;

        if (ballsDestroyed % Random.Range(5, 15) == 0 && BallManager.bm.balls.Count > 0)
            fruits.InstaciateFruit(); 
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
