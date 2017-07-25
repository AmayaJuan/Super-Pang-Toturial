using UnityEngine;

public class ChainGFX : MonoBehaviour
{
    Vector2 startPos;

	void Start ()
    {
        startPos = transform.position;
	}
	
	void Update ()
    {
        transform.position = startPos;
    }
}
