using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelPoints : MonoBehaviour
{
    public Text ballsDestroyed;
    public Text gameScore;
    public Text totalFruits;
    public Text totalTime;
    public Text totalScoreCount;

    int balls;
    int fruits;
    int time;
    int totalScore;

    void OnEnable()
    {
        balls = GameManager.gm.ballsDestroyed;
        ballsDestroyed.text = "X " + balls.ToString();
        fruits = GameManager.gm.fruitsCached;
        totalFruits.text = "X " + fruits.ToString();
        time = (int)GameManager.gm.time + 1;
        totalTime.text = time.ToString() + " Sec";
        SetTotalScore(ScoreManager.sm.currentScore);
        StartCoroutine(TotalScoreAmount());
    }

    void SetTotalScore(int score)
    {
        totalScore += score;
        totalScoreCount.text = totalScore.ToString();
    }

    IEnumerator TotalScoreAmount()
    {
        yield return new WaitForSeconds(1);

        while (balls > 0)
        {
            balls--;
            SetTotalScore(100);
            ballsDestroyed.text = "X " + balls.ToString();
            ScoreManager.sm.UpdateScore(100);
            yield return new WaitForSeconds(.1f);
        }

        while (fruits > 0)
        {
            fruits--;
            SetTotalScore(25);
            totalFruits.text = "X " + fruits.ToString();
            ScoreManager.sm.UpdateScore(25);
            yield return new WaitForSeconds(.1f);
        }

        while (time > 0)
        {
            time--;
            SetTotalScore(15);
            totalTime.text = time.ToString() + " Sec";
            ScoreManager.sm.UpdateScore(15);
            yield return new WaitForSeconds(.05f);
        }

        yield return new WaitForSeconds(1);

        if (SceneManager.sceneCountInBuildSettings > SceneManager.GetActiveScene().buildIndex + 1)
            GameManager.gm.NextLevel();
    }
}
