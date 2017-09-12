using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BorderColor : MonoBehaviour
{
    float time;
    Image borderColor;
    Color normalColor;
    Color dangerColor;
    bool danger;

    void Awake()
    {
        borderColor = GetComponent<Image>();
    }

    void Start ()
    {
        normalColor = borderColor.color;
        dangerColor = new Color((157f / 255f), 0, (20f / 255f));
	}
	
	void Update ()
    {
        time = GameManager.gm.time;

        if (time < 20 && !danger)
            StartCoroutine(Danger());
	}

    IEnumerator Danger()
    {
        danger = true;

        while (time > 0)
        {
            if (borderColor.color == normalColor)
                borderColor.color = dangerColor;
            else
                borderColor.color = normalColor;

            yield return new WaitForSeconds(.3f);
        }
    }
}
