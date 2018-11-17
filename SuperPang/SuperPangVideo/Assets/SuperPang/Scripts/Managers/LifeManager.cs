using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    public GameObject lifeDoll;
    public Text lifesText;
    [HideInInspector]
    public int lifes = 3;

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

    public void SubstractLifes()
    {
        lifes--;
        UpdateLifesText();
    }

    public void UpdateLifesText()
    {
        lifesText.text = "X " + lifes.ToString();
    }

    public void LifesWin()
    {
        animator.SetBool("Win", true);
    }

    public void LifesLose()
    {
        animator.SetBool("Lose", true);
    }

    public void RestartLifesDoll()
    {
        animator.SetBool("Win", false);
        animator.SetBool("Lose", false);
    }
}
