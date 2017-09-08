using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public GameObject[] powerUpsAnimated;
    public Sprite[] powerUpsStatic;

    bool inGround;
    SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Start ()
    {
        int aleatory = Random.Range(0, 2);

        if (aleatory == 0)
        {
            sr.sprite = powerUpsStatic[Random.Range(0, powerUpsStatic.Length)];
            gameObject.name = sr.sprite.name;
        }
	}
	
	void Update ()
    {
        if (!inGround)
            transform.position += Vector3.down * Time.deltaTime * 2;
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            inGround = true;
            Destroy(gameObject, 15);
        }
        if (collision.gameObject.tag == "Player")
        {
            if (gameObject.name.Equals("DoubleArrow"))
                ShotManager.shm.ChangeShot(1);
            else if (gameObject.name.Equals("Ancle"))
                ShotManager.shm.ChangeShot(2);
            else if (gameObject.name.Equals("Gun"))
                ShotManager.shm.ChangeShot(3);

            Destroy(gameObject);
        }     
    }
}
