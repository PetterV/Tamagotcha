using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static bool gameControllerExists = false;
    public System.Random gameRandom = new System.Random();
    public bool gameFrozen = false;
    void Start()
    {
        if (!gameControllerExists)
        {
            DontDestroyOnLoad(gameObject);
            gameControllerExists = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
