using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameMode{PANIC, TOUR};

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public static bool inGame;
    public GameObject ready;
    public GameObject panel;
    public GameMode gameMode;
    [HideInInspector]
    public float time = 100;
    [HideInInspector]
    public int ballsDestroy = 0;
    [HideInInspector]
    public int fruitsCached = 0;
    public Text timeText;
    [HideInInspector]
    public PanelPoints panelPoints;

    int currentLevel = 1;
    Image progressBar;
    Text levelText;
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

        if (SceneManager.GetActiveScene().name.Equals("Panic"))
            gameMode = GameMode.PANIC;
        else
            gameMode = GameMode.TOUR;
    }

    void Start ()
    {
        StartCoroutine(GameStart());
        ScoreManager.sm.UpdateHiScore();

        progressBar = GameObject.FindGameObjectWithTag("Progress").GetComponent<Image>();
        levelText = GameObject.FindGameObjectWithTag("Level").GetComponent<Text>();

        if (gameMode == GameMode.PANIC)
            progressBar.fillAmount = 0;
    }
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.O))
            PlayerPrefs.DeleteAll();

        if (gameMode == GameMode.TOUR)
        {
            if (BallManager.bm.balls.Count == 0 && HexagonManager.hm.hexagons.Count == 0)
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
        else
        {
            if (BallManager.bm.balls.Count == 0 && HexagonManager.hm.hexagons.Count == 0 && BallsSpawn.bs.free)
                BallsSpawn.bs.NewBall();
        }
	}

    public void UpdateBallsDestroyed()
    {
        ballsDestroy++;

        if (ballsDestroy % Random.Range(5, 20) == 0 && BallManager.bm.balls.Count > 0)
            fruits.InstaciateFruit();
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

        if (gameMode == GameMode.TOUR)
        {
            BallManager.bm.StartGame();
            HexagonManager.hm.StartGame();
        }
        else
            BallsSpawn.bs.NewBall();

        inGame = true;
    }

    public int AleatoryNumber()
    {
        return Random.Range(0, 3);
    }

    public void PanicProgress()
    {
        if (gameMode == GameMode.PANIC)
        {
            progressBar.fillAmount += 0.1f;

            if (progressBar.fillAmount == 1)
            {
                progressBar.fillAmount = 0;
                currentLevel++;
                BallsSpawn.bs.IncreaseDificulty();

                if (currentLevel < 10)
                    levelText.text += "Level 0" + currentLevel.ToString();
                else
                    levelText.text += "Level " + currentLevel.ToString();

                FindObjectOfType<BackGroundChange>().BackgroundChange();
            }
        }
    }
}
