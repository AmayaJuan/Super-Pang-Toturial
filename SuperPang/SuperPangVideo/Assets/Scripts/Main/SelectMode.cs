using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectMode : MonoBehaviour
{
    public Image tourModeImage;
    public Image panicModeImage;
    public Text tourModeText;
    public Text panicModeText;

    bool tour;

	void Start ()
    {
        tour = true;
	}
	
	void Update ()
    {
        if (tour)
        {
            tourModeImage.color = new Color(1, 1, 1);
            tourModeText.color = new Color(1, 1, 1);

            panicModeImage.color = new Color(1, 1, 0, .5f);
            panicModeText.color = new Color(1, 1, 0, .5f);

            if (Input.GetKeyDown(KeyCode.RightArrow))
                tour = false;

            if (Input.GetKeyDown(KeyCode.Return))
                SceneManager.LoadScene("Tour_01");
        }
        else
        {
            tourModeImage.color = new Color(1, 1, 0, .5f);
            tourModeText.color = new Color(1, 1, 0, .5f);

            panicModeImage.color = new Color(1, 1, 1);
            panicModeText.color = new Color(1, 1, 1);

            if (Input.GetKeyDown(KeyCode.LeftArrow))
                tour = true;

            if (Input.GetKeyDown(KeyCode.Return))
                SceneManager.LoadScene("Panic");
        }
	}
}
