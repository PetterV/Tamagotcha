using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public static bool musicPlayerExists = false;
    // Start is called before the first frame update
    void Start()
    {
        if (!musicPlayerExists)
        {
            DontDestroyOnLoad(gameObject);
            musicPlayerExists = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
