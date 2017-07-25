using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    void Awake()
    {
        if (gm == null)
            gm = this;
        else if (gm != this)
            Destroy(gameObject);
    }


    void Start ()
    {
        StartCoroutine(Ready());
	}
	
	void Update ()
    {
		
	}

    public IEnumerator Ready()
    {
        StartCoroutine(GameStart());
        yield return null; 
    }

    public IEnumerator GameStart()
    {
        yield return null;
    }
}
