using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    public Text lifesText;
    public GameObject lifeDoll;
    public int lifes = 3;

    Animator animator;

    void Awake()
    {
        animator = lifeDoll.GetComponent<Animator>();
    }

    void Start ()
    {
		
	}
	
	public void UpdateLifes (int life)
    {
        if (life > 0)
            lifes += life;
        else
            lifes -= life;
        lifesText.text = "X " + lifes.ToString();
	}

    public void LifeWin()
    {
        animator.SetBool("Win", true);
    }

    public void LifeLose()
    {
        animator.SetBool("Lose", true);
    }
}
