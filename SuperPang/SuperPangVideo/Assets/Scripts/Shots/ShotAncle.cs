using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotAncle : MonoBehaviour
{
    public GameObject chainGFX;

    float speed = 4f;
    List<GameObject> chains = new List<GameObject>();
    Vector2 startPos;

    void Start()
    {
        startPos = transform.position;
        InstanciateChain();
    }

    void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;

        if ((transform.position.y - startPos.y) >= .2f)
            InstanciateChain();
    }

    void InstanciateChain()
    {
        GameObject chain = Instantiate(chainGFX, transform.position, Quaternion.identity);
        chain.transform.parent = transform;
        chains.Add(chain);
        startPos = transform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Roof")
            StartCoroutine(DestroyAncle());

        if (other.gameObject.tag == "Ball")
        {
            other.gameObject.GetComponent<Ball>().Split();
            AncleDestroy();
        }

        if (other.gameObject.tag == "Hexagon")
        {
            other.gameObject.GetComponent<Hexagon>().Split();
            AncleDestroy();
        }
    }

    void AncleDestroy()
    {
        Destroy(gameObject);
        ShotManager.shm.DestroyShot();
    }

    IEnumerator DestroyAncle()
    {
        speed = 0;
        yield return new WaitForSeconds(1);
        GetComponentInParent<SpriteRenderer>().color = Color.red;

        foreach (GameObject item in chains)
            item.GetComponent<SpriteRenderer>().color = Color.red;

        yield return new WaitForSeconds(1);
        Destroy(gameObject);
        ShotManager.shm.DestroyShot();
    }
}
