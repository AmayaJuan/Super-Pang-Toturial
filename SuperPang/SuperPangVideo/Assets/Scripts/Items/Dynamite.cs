using UnityEngine;

public class Dynamite : MonoBehaviour
{
    bool inGround;

    void Update()
    {
        if (!inGround)
            transform.position += Vector3.down * 2 * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            inGround = true;
            Destroy(gameObject, 15f);
        }

        if (other.gameObject.tag == "Player")
        { 
            Destroy(gameObject);
            BallManager.bm.Dynamite(5);
        }
    }
}
