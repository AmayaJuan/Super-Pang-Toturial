using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public static DontDestroy dd;

    void Awake()
    {
        if (dd == null)
            dd = this;
        else if (dd != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}
