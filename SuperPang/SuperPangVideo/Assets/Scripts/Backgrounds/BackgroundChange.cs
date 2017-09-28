using UnityEngine;
using UnityEngine.UI;

public class BackgroundChange : MonoBehaviour
{
    public Sprite[] backgrounds;

    Image currentBackground;

	void Start ()
    {
        currentBackground = GetComponent<Image>();
        currentBackground.sprite = backgrounds[Random.Range(0, backgrounds.Length)];
	}
}
