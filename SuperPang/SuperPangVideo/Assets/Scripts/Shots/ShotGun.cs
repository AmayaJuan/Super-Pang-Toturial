using UnityEngine;

public class ShotGun : MonoBehaviour
{
    float speed = 8f;
	
	void Update ()
    {
        if (transform.rotation.z == 0)
            transform.position += Vector3.up * Time.deltaTime * speed;
        else if (transform.rotation.z < 0)
            transform.position += new Vector3(.1f, 1) * Time.deltaTime * speed;
        else
            transform.position += new Vector3(-.1f, 1) * Time.deltaTime * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ball")
            other.gameObject.GetComponent<Ball>().Split();
        if (other.gameObject.tag != "Player")
        {
            Destroy(gameObject);
            ShotManager.shm.DestroyShot();
        }
    }
}
