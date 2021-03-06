﻿using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager sm;
    public Text scoreText;
    public Text hiScoreText;
    [HideInInspector]
    public int currentScore;
    [HideInInspector]
    public int hiScore = 500;

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

        if (currentScore > hiScore)
        {
            hiScore = currentScore;
            hiScoreText.text = "HI - " + hiScore.ToString();
            PlayerPrefs.SetInt("HiScore", hiScore);
        }
	}

    public void UpdateHiScore()
    {
        hiScore = PlayerPrefs.GetInt("HiScore");
        hiScoreText.text = "HI - " + hiScore.ToString();
    }
}
