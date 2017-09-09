using UnityEngine;

public class Fruits : MonoBehaviour
{
    public GameObject fruitItem;
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.F))
            InstaciateFruit();
	}

    public void InstaciateFruit()
    {
        Instantiate(fruitItem, transform.position, Quaternion.identity);
    }
}
