using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpText : MonoBehaviour
{
    public Vector2 startPos;

	void Start ()
    {
        startPos = transform.position;
	}
	
	void Update ()
    {
        transform.Translate(Vector3.up * 2 * Time.deltaTime);

        if (transform.position.y > startPos.y + 2)
            Destroy(gameObject);
	}
}
