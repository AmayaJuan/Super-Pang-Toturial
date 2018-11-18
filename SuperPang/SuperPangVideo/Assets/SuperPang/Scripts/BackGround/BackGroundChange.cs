using UnityEngine;
using UnityEngine.UI;

public class BackGroundChange : MonoBehaviour
{
    public Sprite[] backgrounds;

    Image currentBackground;

	void Start ()
    {
        BackgroundChange();
	}

    public void BackgroundChange()
    {
        currentBackground = GetComponent<Image>();
        currentBackground.sprite = backgrounds[Random.Range(0, backgrounds.Length)];
    }
}
