using UnityEngine;
using UnityEngine.UI;

public class BackgroundChange : MonoBehaviour
{
    public Sprite[] backgrounds;

    Image currentBackground;

	void Start ()
    {
        BackgroundChanges();
    }

    public void BackgroundChanges()
    {
        currentBackground = GetComponent<Image>();
        currentBackground.sprite = backgrounds[Random.Range(0, backgrounds.Length)];
    }
}
