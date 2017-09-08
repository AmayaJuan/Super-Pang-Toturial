using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotAncle : MonoBehaviour
{
    public GameObject chainGFX;

    float speed = 4;
    List<GameObject> chains = new List<GameObject>();
    Vector2 startPos;

    void Start()
    {
        startPos = transform.position;
        InstantiateChain();
    }

    void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;

        if ((transform.position.y - startPos.y) >= .2f)
            InstantiateChain();
    }

    void InstantiateChain()
    {
        GameObject chain = Instantiate(chainGFX, transform.position, Quaternion.identity);
        chain.transform.parent = transform;
        chains.Add(chain);
        startPos = transform.position;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Roof")
            StartCoroutine(DestroyAncle());

        if (collision.gameObject.tag == "Ball")
        {
            collision.gameObject.GetComponent<Ball>().Split();
            Destroy(gameObject);
            ShotManager.shm.DestroyShot();
        }
    }

    IEnumerator DestroyAncle()
    {
        speed = 0;
        yield return new WaitForSeconds(1);
        GetComponentInParent<SpriteRenderer>().color = Color.red; // new Color(1, 0, 0, 1)

        foreach (GameObject item in chains)
            item.GetComponent<SpriteRenderer>().color = Color.red; // new Color(1, 0, 0, 1)

        yield return new WaitForSeconds(1);
        Destroy(gameObject);
        ShotManager.shm.DestroyShot();
    }
}
