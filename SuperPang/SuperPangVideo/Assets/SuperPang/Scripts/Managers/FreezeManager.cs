using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FreezeManager : MonoBehaviour
{
    public static FreezeManager fm;
    public Text freezeTimeText;
    public GameObject freezeTimeCount;
    [HideInInspector]
    public float freezeTime;
    [HideInInspector]
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

    public void StartFreeze(float time)
    {
        freezeTime = time;

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

        foreach (GameObject item in HexagonManager.hm.hexagons)
        {
            if (item != null)
                item.GetComponent<Hexagon>().FreezeHexagon(item);
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

        foreach (GameObject item in HexagonManager.hm.hexagons)
        {
            if (item != null)
                item.GetComponent<Hexagon>().UnFreezeHexagon(item);
        }

        freeze = false;
    }
}
