using UnityEngine;

public class ShotGun : MonoBehaviour
{
    float speed = 8;

	void Update ()
    {
        if (transform.rotation.z == 0)
            transform.position += Vector3.up * Time.deltaTime * speed;
        else if (transform.rotation.z < 0)
            transform.position += new Vector3(.1f, 1) * Time.deltaTime * speed;
        else
            transform.position += new Vector3(-.1f, 1) * Time.deltaTime * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball")
            collision.gameObject.GetComponent<Ball>().Split();

        Destroy(gameObject);
        ShotManager.shm.DestroyShot();

    }
}
