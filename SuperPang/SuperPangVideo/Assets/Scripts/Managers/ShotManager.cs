﻿using UnityEngine;

public class ShotManager : MonoBehaviour
{
    public static ShotManager shm;
    public int maxShots;
    public int numberOfShots = 0;
    public int typeOfShot; // 0 - Arrow. 1 - Dpuble Arrow. 2 - Ancle. 3 - Laser 
    public GameObject[] shots;

    Animator animator;
    Transform player;

    void Awake()
    {
        if (shm == null)
            shm = this;
        else if (shm != this)
            Destroy(gameObject);
        player = FindObjectOfType<Player>().transform;
    }

    void Start ()
    {
        maxShots = 1;
        typeOfShot = 0;
	}
	
	void Update ()
    {
        if (CanShot() && Input.GetKeyDown(KeyCode.X))
            Shot();
	}

    bool CanShot()
    {
        if (numberOfShots < maxShots)
            return true;
        return false;
    }

    void Shot()
    {
        if (typeOfShot != 3)
            Instantiate(shots[typeOfShot], player.position, Quaternion.identity);
        else
        {
            Instantiate(shots[3], new Vector2(player.position.x + .5f, player.position.y + 1), Quaternion.Euler(new Vector3(0, 0, -5)));
            Instantiate(shots[3], new Vector2(player.position.x, player.position.y + 1), Quaternion.identity);
            Instantiate(shots[3], new Vector2(player.position.x - .5f, player.position.y + 1), Quaternion.Euler(new Vector3(0, 0, 5)));
        }
       
        numberOfShots++;
    }

    public void DestroyShot()
    {
        if (numberOfShots > 0 && numberOfShots <= maxShots)
            numberOfShots--;
    }

    public void ChangeShot(int type)
    {
        if (typeOfShot != type)
        {
            switch (type)
            {
                case 0:
                    maxShots = 1;
                    break;

                case 1:
                    maxShots = 2;
                    break;

                case 2:
                    maxShots = 1;
                    break;

                case 3:
                    maxShots = 15;
                    break;
            }

            typeOfShot = type;
            numberOfShots = 0;
        }
    }
}
