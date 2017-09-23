using UnityEngine;

public class FruitItem : MonoBehaviour
{
    public Sprite[] fruitSprites;

    bool inGround;
    SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Start ()
    {
        sr.sprite = fruitSprites[Random.Range(0, fruitSprites.Length)];
        gameObject.name = sr.sprite.name;
	}
	
	void Update ()
    {
        if (!inGround)
            transform.position += Vector3.down * 2 * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            inGround = true;
            Destroy(gameObject, 15);
        }
        else if (other.gameObject.tag == "Player" || other.gameObject.tag == "Arrow" || other.gameObject.tag == "Ancle")
        {
            int score = Random.Range(500, 1000);
            ScoreManager.sm.UpdateScore(score);
            PopUpManager.pop.InstanciatePopUpText(transform.position, score);
            Destroy(gameObject);
        }
    }
}
