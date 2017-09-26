using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public static bool inGame;
    public Text timeText; 
    public GameObject ready;
    public GameObject panel;
    [HideInInspector]
    public int fruitsCached = 0;
    [HideInInspector]
    public int ballsDestroyed = 0;
    [HideInInspector]
    public float time = 100f;

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

        fruits = FindObjectOfType<Fruits>();
        lm = FindObjectOfType<LifeManager>();
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
