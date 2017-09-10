using UnityEngine;

public class CurrentShotImage : MonoBehaviour
{
    public GameObject arrowShot;
    public GameObject ancleShot;
    public GameObject gunShot;

    public void CurrentShot(string shot)
    {
        if (shot.Equals("Arrow"))
        {
            arrowShot.SetActive(true);
            ancleShot.SetActive(false);
            gunShot.SetActive(false);
        }
        else if (shot.Equals("Ancle"))
        {
            ancleShot.SetActive(true);
            arrowShot.SetActive(false);
            gunShot.SetActive(false);
        }
        else if (shot.Equals("Gun"))
        {
            gunShot.SetActive(true);
            ancleShot.SetActive(false);
            arrowShot.SetActive(false);
        }
        else
        {
            arrowShot.SetActive(false);
            ancleShot.SetActive(false);
            gunShot.SetActive(false);
        }
    }
}
