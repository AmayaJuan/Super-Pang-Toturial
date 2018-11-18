using UnityEngine;

public class ShotArrow : MonoBehaviour
{
    public GameObject chainGFX;

    float speed = 4f;
    Vector2 startPos;

    void Start()
    {
        startPos = transform.position;
        InstanceChain();
    }

    void Update ()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;

        if ((transform.position.y - startPos.y) >= 0.2f)
            InstanceChain();
    }

    void InstanceChain()
    {
        GameObject chain = Instantiate(chainGFX, transform.position, Quaternion.identity);
        chain.transform.parent = transform;
        startPos = transform.position;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball")
            collision.gameObject.GetComponent<Ball>().Split();

        if (collision.gameObject.tag == "Hexagon")
            collision.gameObject.GetComponent<Hexagon>().Split();

        if (GameManager.gm.gameMode == GameMode.TOUR || GameManager.gm.gameMode == GameMode.PANIC && BallsSpawn.bs.free)
        {
            if (collision.gameObject.name.Contains("Special"))
                FreezeManager.fm.StartFreeze(1.5f);
        }

        Destroy(gameObject);
        ShotManager.shm.DestroyShot(); 
    }
}
