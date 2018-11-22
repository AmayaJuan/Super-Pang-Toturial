using UnityEngine;
using UnityEngine.SceneManagement;

public class PressStart : MonoBehaviour
{
    public GameObject pressStart;

    float time;

    void Start()
    {
        GameObject destroyOnLoad = FindObjectOfType<DontDestroy>().gameObject;

        if (destroyOnLoad != null)
            Destroy(destroyOnLoad);
        
    }

    void Update ()
    {
        time += Time.deltaTime; //time = time + Time.deltaTime

        if (Mathf.RoundToInt(time) % 2 == 0)
            pressStart.SetActive(true);
        else
            pressStart.SetActive(false);

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
            SceneManager.LoadScene(1);
	}
}
