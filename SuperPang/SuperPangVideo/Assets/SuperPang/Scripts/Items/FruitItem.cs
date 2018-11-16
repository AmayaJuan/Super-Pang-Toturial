using UnityEngine;

public class FruitItem : MonoBehaviour
{
    public Sprite[] fruitsSprites;

    bool inGround;
    SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Start ()
    {
        sr.sprite = fruitsSprites[Random.Range(0, fruitsSprites.Length)];
        gameObject.name = sr.sprite.name;
	}
	
	void Update ()
    {
        if (!inGround)
            transform.position += Vector3.down * Time.deltaTime * 2;
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            inGround = true;
            Destroy(gameObject, 15);
        }
        else if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Arrow" || collision.gameObject.tag == "Ancle")
        {
            int score = Random.Range(500, 1000);
            ScoreManager.sm.UpdateScore(score);
            PopUpManager.pm.IntanciatePopUpText(transform.position, score);
            GameManager.gm.fruitsCached++;
            Destroy(gameObject);
        }
    }
}
