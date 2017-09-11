using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    public GameObject lifeDoll;
    public int lifes = 3;
    public Text lifesText;

    Animator animator;

    void Awake()
    {
        animator = lifeDoll.GetComponent<Animator>();
    }
	
	public void UpdateLifes (int life)
    {
        if (life > 0)
            lifes += life;
        else
            lifes -= life;

        lifesText.text = "X " + lifesText.ToString();
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
