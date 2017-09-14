using UnityEngine;
using UnityEngine.SceneManagement;

public class PressStart : MonoBehaviour
{
    public GameObject pressStart;

    float time;
	
	void Update ()
    {
        time += Time.deltaTime;

        if (Mathf.RoundToInt(time) % 2 == 0)
            pressStart.SetActive(true);
        else
            pressStart.SetActive(false);

        if (Input.GetMouseButtonDown(0))
            SceneManager.LoadScene(1);
	}
}
