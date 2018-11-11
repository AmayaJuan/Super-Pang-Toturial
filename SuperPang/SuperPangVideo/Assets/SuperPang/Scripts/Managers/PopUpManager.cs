using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    public static PopUpManager pm;
    public GameObject popUpText;

    void Awake()
    {
        if (pm == null)
            pm = this;
        else if (pm != this)
            Destroy(gameObject);
    }

    public void IntanciatePopUpText(Vector2 starPos, int textScore)
    {
        GameObject pop = Instantiate(popUpText);
        pop.transform.position = starPos;
        pop.GetComponent<TextMesh>().text = textScore.ToString();
    }
}
