﻿using UnityEngine;

public class ShotArrow : MonoBehaviour
{
    float speed = 4;

	void Start ()
    {
		
	}

	void Update ()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        ShotManager.shm.DestroyShot();
    }
}
