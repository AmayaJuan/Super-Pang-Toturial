using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectMode : MonoBehaviour
{
    public Image tourModeImage;
    public Image panicModeImage;
    public Text tourModeText;
    public Text panicModeText;

    bool tour = true;

    void Update()
    {
        if (tour)
        {
            tourModeImage.color = new Color(1, 1, 1);
            tourModeText.color = Color.white;

            panicModeImage.color = new Color(1, 1, 0, .5f);
            panicModeText.color = new Color(1, 1, 0, .5f);

            if (Input.GetKeyDown(KeyCode.RightArrow))
                tour = !tour;

            if (Input.GetKeyDown(KeyCode.Return))
                SceneManager.LoadScene("Tour_01");
        }
        else
        {
            panicModeImage.color = new Color(1, 1, 1);
            panicModeText.color = Color.white;

            tourModeImage.color = new Color(1, 1, 0, .5f);
            tourModeText.color = new Color(1, 1, 0, .5f);

            if (Input.GetKeyDown(KeyCode.LeftArrow))
                tour = !tour;

            if (Input.GetKeyDown(KeyCode.Return))
                SceneManager.LoadScene("Panic");
        }
    }
    /*
    public void NextNivel(string name)
    {
        SceneManager.LoadScene(name);
    }
    */
}
