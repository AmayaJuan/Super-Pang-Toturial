using UnityEngine;

public class ShotManager : MonoBehaviour
{
    public static ShotManager shm;
    public GameObject[] shots;

    int maxShots;
    int numberOfShots = 0;
    int typeOfShot; // 0 - Arrow // 1- Double Arrow // 2 - Ancle // 3 - Laser
    Animator animator;
    Transform player;

    void Awake()
    {
        if (shm == null)
            shm = this;
        else if (shm != this)
            Destroy(gameObject);

        animator = GetComponent<Animator>();
        player = FindObjectOfType<Player>().transform;
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
	}

    bool CanShot()
    {
        if (numberOfShots < maxShots)
            return true;
        return false;
    }

    void Shot()
    {
        Instantiate(shots[typeOfShot], player.position, Quaternion.identity);
        numberOfShots++;
    }

    public void DestroyShot()
    {
        if (numberOfShots > 0 && numberOfShots < maxShots)
            numberOfShots--;
    }
}
