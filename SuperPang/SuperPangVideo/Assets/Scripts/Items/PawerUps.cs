using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawerUps : MonoBehaviour
{
    public Sprite[] powerUpsStatic;
    public GameObject[] powerUpsAnimated;

    bool inGround;
    SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Start ()
    {
        int aleatory = Random.Range(0, 2);

        if (aleatory == 0)
        {
            sr.sprite = powerUpsStatic[Random.Range(0, powerUpsStatic.Length)];
            gameObject.name = sr.sprite.name;
        }
        else
        {
            Instantiate(powerUpsAnimated[Random.Range(0, powerUpsAnimated.Length)], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
	}

    void Update()
    {
        if (!inGround)
            transform.position += Vector3.down * Time.deltaTime * 2;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            inGround = true;
            Destroy(gameObject, 60);
        }

        if (other.gameObject.tag == "Player")
        {
            if (gameObject.name.Equals("DoubleArrow"))
                ShotManager.shm.ChangeShot(1);
            else if (gameObject.name.Equals("Ancle"))
                ShotManager.shm.ChangeShot(2);
            else if (gameObject.name.Equals("Gun"))
                ShotManager.shm.ChangeShot(3);
            else if (gameObject.name.Equals("TimeStop"))
                FreezeManager.fm.StartFreeze();

            Destroy(gameObject);
        }
    }
}
