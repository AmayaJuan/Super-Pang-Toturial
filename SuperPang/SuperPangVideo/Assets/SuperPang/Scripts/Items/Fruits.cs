using UnityEngine;

public class Fruits : MonoBehaviour
{
    public GameObject fruitItem;

    public void InstaciateFruit()
    {
        int fruitsInGame = GameObject.FindGameObjectsWithTag("Fruit").Length;

        if (fruitsInGame == 0)
            Instantiate(fruitItem, transform.position, Quaternion.identity);
    }
}
