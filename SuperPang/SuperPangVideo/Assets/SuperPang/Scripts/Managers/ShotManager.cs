using UnityEngine;

public class ShotManager : MonoBehaviour
{
    public static ShotManager shm;
    public GameObject[] shots;
    public int maxShots;
    public int numberOfShots = 0;
    public int typeOfShot; //0- Arrow //1- Double Arrow //2- Ancle //3- Laser

    Transform player;
    Animator anim;

    void Awake()
    {
        if (shm == null)
            shm = this;
        else if (shm != this)
            Destroy(gameObject);

        player = FindObjectOfType<Player>().transform;
    }

    void Start ()
    {
        typeOfShot = 0;
        maxShots = 1;
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
        if (numberOfShots > 0 && numberOfShots <= maxShots)
            numberOfShots--;
    }
}
