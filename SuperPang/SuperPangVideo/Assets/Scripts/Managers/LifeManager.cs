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
	
	public void AmountLifes ()
    {
        lifes++;
        UpdateLifesText();
    }

    public void SubstracLifes()
    {
        lifes--;
        UpdateLifesText();
    }

    public void UpdateLifesText()
    {
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

    public void RestartLifesDoll()
    {
        animator.SetBool("Win", false);
        animator.SetBool("Lose", false);
    }
}
