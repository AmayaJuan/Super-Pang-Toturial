using UnityEngine;
using UnityEngine.EventSystems;

public class Buttons : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public static bool shotButton;
    Player player;

    void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (gameObject.name.Equals("Right"))
            player.horizontal = 1;
        else if (gameObject.name.Equals("Left"))
            player.horizontal = -1;
        else if (gameObject.name.Equals("Up"))
            player.vertical = 1;
        else if (gameObject.name.Equals("Down"))
            player.vertical = -1;
        else if (gameObject.name.Equals("Shot"))
            shotButton = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (gameObject.name.Equals("Right") || gameObject.name.Equals("Left"))
            player.horizontal = 0;
        else if (gameObject.name.Equals("Up") || gameObject.name.Equals("Down"))
            player.vertical = 0;
        else if (gameObject.name.Equals("Shot"))
            shotButton = false;
    }
}
