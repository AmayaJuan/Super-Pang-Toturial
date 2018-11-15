using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager sm;
    public Text scoreText;

    int currentScore;

    void Awake()
    {
        if (sm == null)
            sm = this;
        else if (sm != this)
            Destroy(gameObject);
    }

    void Start ()
    {
        currentScore = 0;
        scoreText.text = currentScore.ToString();
	}
	
	public void UpdateScore (int score)
    {
        currentScore += score;
        scoreText.text = currentScore.ToString();
	}
}
