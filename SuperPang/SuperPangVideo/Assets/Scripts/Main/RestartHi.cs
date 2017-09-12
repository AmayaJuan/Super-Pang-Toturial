using UnityEngine;

public class RestartHi : MonoBehaviour
{
    public void Reset(bool restart)
    {
        if (restart)
            PlayerPrefs.DeleteAll();
    }
}
