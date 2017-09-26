using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    public static PopUpManager pop;
    public GameObject popUpText;

    void Awake()
    {
        if (pop == null)
            pop = this;
        else if (pop != this)
            Destroy(gameObject);
    }

    public void InstanciatePopUpText(Vector2 startPos, int textScore)
    {
        GameObject pop = Instantiate(popUpText);
        pop.transform.position = startPos;
        pop.GetComponent<TextMesh>().text = textScore.ToString();
    }
}
