using UnityEngine;

public class DDOL : MonoBehaviour
{
    private static bool governerExists = false;

    private void Start()
    {
        if (!governerExists)
        {
            DontDestroyOnLoad(transform.gameObject);
            governerExists = true;
        }
        else
            Destroy(gameObject);
    }
}
