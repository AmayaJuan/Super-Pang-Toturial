using UnityEngine;

public class ShotManager : MonoBehaviour
{
    public static ShotManager shm;
    public GameObject[] shots;
    public int maxShots;
    public int numberOfShots = 0;
    public int typeOfShot; // 0 - Arrow // 1- Double Arrow // 2 - Ancle // 3 - Laser

    Animator animator;
    CurrentShotImage shotImage;
    Transform player;

    void Awake()
    {
        if (shm == null)
            shm = this;
        else if (shm != this)
            Destroy(gameObject);

        animator = GetComponent<Animator>();
        player = FindObjectOfType<Player>().transform;
        shotImage = FindObjectOfType<CurrentShotImage>();
    }

    void Start ()
    {
        maxShots = 1;
        typeOfShot = 0;
	}
	
	void Update ()
    {
        if (CanShot() && Input.GetKeyDown(KeyCode.X))
            Shot();
        if (numberOfShots == maxShots && GameObject.FindGameObjectsWithTag("Arrow").Length == 0 && GameObject.FindGameObjectsWithTag("Ancle").Length == 0)
            numberOfShots = 0;
	}

    bool CanShot()
    {
        if (numberOfShots < maxShots)
            return true;
        return false;
    }

    void Shot()
    {
        if (typeOfShot != 3)
            Instantiate(shots[typeOfShot], player.position, Quaternion.identity);
        else
        {
            Instantiate(shots[typeOfShot], new Vector2(player.position.x + .5f, player.position.y + 1), Quaternion.Euler(new Vector3(0, 0, -5)));
            Instantiate(shots[typeOfShot], new Vector2(player.position.x, player.position.y + 1), Quaternion.identity);
            Instantiate(shots[typeOfShot], new Vector2(player.position.x - .5f, player.position.y + 1), Quaternion.Euler(new Vector3(0, 0, 5)));
        }

        numberOfShots++;
    }

    public void DestroyShot()
    {
        if (numberOfShots > 0 && numberOfShots < maxShots)
            numberOfShots--;
    }

    public void ChangeShot(int type)
    {
        if (typeOfShot != type)
        {
            switch(type)
            {
                case 0:
                    maxShots = 1;
                    shotImage.CurrentShot("");
                    break;

                case 1:
                    maxShots = 2;
                    shotImage.CurrentShot("Arrow");
                    break;

                case 2:
                    maxShots = 1;
                    shotImage.CurrentShot("Ancle");
                    break;

                case 3:
                    maxShots = 15;
                    shotImage.CurrentShot("Gun");
                    break;
            }

            typeOfShot = type;
            numberOfShots = 0;
        }
    }
}
