using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager sm;
    public Text scoreText;

    int curentScore = 0;


    void Awake()
    {
        if (sm == null)
            sm = this;
        else if (sm != this)
            Destroy(gameObject);
    }

    void Start ()
    {
        curentScore = 0;
        scoreText.text = curentScore.ToString();
	}
	
	public void UpdateScore (int score)
    {
        curentScore += score;
        scoreText.text = curentScore.ToString();
    }
}
