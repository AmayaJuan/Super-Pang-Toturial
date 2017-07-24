using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotArrow : MonoBehaviour
{
    public GameObject chainGFX;

    float speed = 4f;
    Vector2 startPos;

	void Start ()
    {
        startPos = transform.position;
        InstantiateChainGFX();
    }
	
	void Update ()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;

        if ((transform.position.y - startPos.y) >= .2f)
            InstantiateChainGFX();
	}

    void InstantiateChainGFX()
    {
        GameObject chain = Instantiate(chainGFX, transform.position, Quaternion.identity);
        chain.transform.parent = transform;
        startPos = transform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
        ShotManager.shm.DestroyShot();
    }
}
