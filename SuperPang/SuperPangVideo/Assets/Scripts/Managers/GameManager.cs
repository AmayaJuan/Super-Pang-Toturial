using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameMode {PANIC, TOUR};

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public static bool inGame;
    public Text timeText;
    public GameObject ready;
    public GameObject panel;
    public GameObject gameOver;
    [HideInInspector]
    public int fruitsCached = 0;
    [HideInInspector]
    public int ballsDestroyed = 0;
    [HideInInspector]
    public float time = 100f;
    public GameMode gameMode;

    int currentLevel = 1;
    Fruits fruits;
    Image progressBar;
    LifeManager lm;
    PanelPoints panelPoints;
    Player player;
    Text levelText;

    void Awake()
    {
        if (gm == null)
            gm = this;
        else if (gm != this)
            Destroy(gameObject);

        fruits = FindObjectOfType<Fruits>();
        lm = FindObjectOfType<LifeManager>();
        player = FindObjectOfType<Player>();

        if (SceneManager.GetActiveScene().name.Equals("Panic"))
        {
            gameMode = GameMode.PANIC;
            progressBar = GameObject.FindGameObjectWithTag("Progress").GetComponent<Image>();
            levelText = GameObject.FindGameObjectWithTag("Level").GetComponent<Text>();
        }
        else
            gameMode = GameMode.TOUR;
    }

    void Start ()
    {
        StartCoroutine(GameStart());
        ScoreManager.sm.UpdateHiScore();
        gameOver.SetActive(false); 

        if (gameMode == GameMode.PANIC)
            progressBar.fillAmount = 0;
    }
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.O))
            PlayerPrefs.DeleteAll();

        if (gameMode == GameMode.TOUR)
        {
            if (HexagonManager.hm.hexagons.Count == 0 && BallManager.bm.balls.Count == 0)
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
        else
        {
            if (HexagonManager.hm.hexagons.Count == 0 && BallManager.bm.balls.Count == 0 && BallHexagonSpawn.bs.free)
                BallHexagonSpawn.bs.NewBall();
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
        lm.RestartLifesDoll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void StartGameOver()
    {
        StartCoroutine(GameStart());
    }

    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(2);
        ready.SetActive(false);

        if (gameMode == GameMode.TOUR)
        {
            BallManager.bm.StartGame();
            HexagonManager.hm.StartGame();
        }
        else
            BallHexagonSpawn.bs.NewBall();
        
        inGame = true;
    }

    public IEnumerator GameOver()
    {
        gameOver.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);
    }

    public int AleatoryNumber()
    {
        return Random.Range(0, 3);
    }

    public void PanicProgress()
    {
        if (gameMode == GameMode.PANIC)
        {
            progressBar.fillAmount += .1f;

            if (progressBar.fillAmount == 1)
            {
                progressBar.fillAmount = 0;
                currentLevel++;
                BallHexagonSpawn.bs.IncreaseDificulty();

                if (currentLevel < 10)
                    levelText.text = "LEVEL 0" + currentLevel.ToString();
                else
                    levelText.text = "LEVEL " + currentLevel.ToString();

                FindObjectOfType<BackgroundChange>().BackgroundChanges();
            }
        }
    }
}
