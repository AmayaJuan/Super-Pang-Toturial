using UnityEngine;

public class ShotArrow : MonoBehaviour
{
    public GameObject chainGFX;

    float speed = 4f;
    Vector2 startPos;

	void Start ()
    {
        startPos = transform.position;
        InstanciateChain();
    }
	
	void Update ()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;

        if ((transform.position.y - startPos.y) >= .2f)
            InstanciateChain();
    }

    void InstanciateChain()
    {
        GameObject chain = Instantiate(chainGFX, transform.position, Quaternion.identity);
        chain.transform.parent = transform;
        startPos = transform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ball")
            other.gameObject.GetComponent<Ball>().Split();

        //if (other.gameObject.tag != "Player")
       // {
            Destroy(gameObject);
            ShotManager.shm.DestroyShot();
        //}
    }
}
