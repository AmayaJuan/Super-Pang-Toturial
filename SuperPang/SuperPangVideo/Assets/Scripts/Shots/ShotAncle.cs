using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotAncle : MonoBehaviour
{
    public GameObject chainGFX;

    float speed = 4f;
    Vector2 startPos;
    List<GameObject> chains = new List<GameObject>();

    void Start()
    {
        startPos = transform.position;
        InstantiateChainGFX();
    }

    void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
        if ((transform.position.y - startPos.y) >= .2f)
            InstantiateChainGFX();
    }

    void InstantiateChainGFX()
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
    }

    IEnumerator DestroyAncle()
    {
        speed = 0;
        yield return new WaitForSeconds(1);
        GetComponentInParent<SpriteRenderer>().color = Color.red;

        foreach (GameObject item in chains)
            item.GetComponent<SpriteRenderer>().color = Color.red;//new Color(1, 0, 0, 1)

        yield return new WaitForSeconds(1);
        Destroy(gameObject);
        ShotManager.shm.DestroyShot();
    }
}
