using UnityEngine;

public class ShotArrow : MonoBehaviour
{
    public GameObject chainGFX;

    float speed = 4;
    Vector2 startPos;

    void Start()
    {
        startPos = transform.position;
        InstantiateChain();
    }

    void Update ()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;

        if ((transform.position.y - startPos.y) >= .2f)
            InstantiateChain();
    }

    void InstantiateChain()
    {
        GameObject chain = Instantiate(chainGFX, transform.position, Quaternion.identity);
        chain.transform.parent = transform;
        startPos = transform.position;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Destroy"))
            BallManager.bm.Dynamite(6);

        if (collision.gameObject.name.Equals("StopBall"))
            FreezeManager.fm.StartFreeze(6);

        if (collision.gameObject.tag == "Ball")
            collision.gameObject.GetComponent<Ball>().Split();

        if (collision.gameObject.tag == "Hexagon")
            collision.gameObject.GetComponent<Hexagon>().Split();

        Destroy(gameObject);
        ShotManager.shm.DestroyShot();    
    }
}
