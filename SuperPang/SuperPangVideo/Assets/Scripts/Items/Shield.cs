using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    bool inGround;

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
            other.gameObject.GetComponent<Player>().shield.SetActive(true);
            other.gameObject.GetComponent<Player>().blink = false;
            Destroy(gameObject);
        }
    }
}
