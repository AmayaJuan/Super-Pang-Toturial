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
        if (lifes > 0)
            lifes += life;
        else
            lifes -= life;

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
}
