using UnityEngine;

public class Fruits : MonoBehaviour
{
    public GameObject fruitItem;

    public void InstaciateFruit()
    {
        Instantiate(fruitItem, transform.position, Quaternion.identity);
    }
}
