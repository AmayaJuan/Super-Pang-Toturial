using UnityEngine;

public class Chain : MonoBehaviour
{
	void Update ()
    {
        if (transform.localScale.y < 7f)
            transform.localScale += Vector3.up * Time.deltaTime * 4f;
	}
}
