using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FreezeManager : MonoBehaviour
{
    public static FreezeManager fm;
    public Text freezeTimeText;
    public GameObject freezeTimeCount;
    public float freezeTime;
    public bool freeze;

    void Awake()
    {
        if (fm == null)
            fm = this;
        else if (fm != this)
            Destroy(gameObject);
    }

    void Start ()
    {
        freezeTimeCount.SetActive(false);
	}
	
	void Update ()
    {
		
	}

    public void StartFreeze()
    {
        freezeTime = 3;

        if (!freeze)
            StartCoroutine(FreezeTime());
    }

    public IEnumerator FreezeTime()
    {
        freeze = true;

        foreach (GameObject item in BallManager.bm.balls)
        {
            if (item != null)
                item.GetComponent<Ball>().FreezeBall(item);
        }

        freezeTimeCount.SetActive(true);

        while (freezeTime > 0)
        {
            freezeTime -= Time.deltaTime;
            freezeTimeText.text = freezeTime.ToString("f2");
            yield return null;
        }

        freezeTimeCount.SetActive(false);
        freezeTime = 0;

        foreach (GameObject item in BallManager.bm.balls)
        {
            if (item != null)
                item.GetComponent<Ball>().UnFreezeBall(item);
        }

        freeze = false;
    }
}
