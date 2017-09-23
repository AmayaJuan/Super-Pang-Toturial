using System.Collections;
using System.Collections.Generic;
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
            arrowShot.SetActive(false);
            ancleShot.SetActive(true);
            gunShot.SetActive(false);
        }
        else if (shot.Equals("Gun"))
        {
            arrowShot.SetActive(false);
            ancleShot.SetActive(false);
            gunShot.SetActive(true);
        }
        else
        {
            arrowShot.SetActive(false);
            ancleShot.SetActive(false);
            gunShot.SetActive(false);
        }
    }
}
