using UnityEngine;

public class PressStart : MonoBehaviour
{
    public GameObject pressStart;

    float time;

	void Update ()
    {
        time += Time.deltaTime; //time = time + Time.deltaTime

        if (Mathf.RoundToInt(time) % 2 == 0)
            pressStart.SetActive(true);
        else
            pressStart.SetActive(false);
	}
}
