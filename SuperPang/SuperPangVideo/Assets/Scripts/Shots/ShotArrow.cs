using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotArrow : MonoBehaviour
{
    float speed = 4f;

	void Start ()
    {
		
	}
	
	void Update ()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
        ShotManager.shm.DestroyShot();
    }
}
