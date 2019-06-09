using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public void StartOver()
    {
        SceneManager.LoadScene(0);
        WorldMethods.GetGameController().gameFrozen = false;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
        WorldMethods.GetGameController().gameFrozen = false;
    }
}
