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
	
	public void AmountLife()
    {
        lifes++;
        UpdateLifesText();
	}

    public void SubstracLifes()
    {
        lifes--;
        UpdateLifesText();
    }

    void UpdateLifesText()
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
