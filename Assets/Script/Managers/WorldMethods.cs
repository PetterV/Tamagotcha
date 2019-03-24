using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMethods : MonoBehaviour
{
    public static GameController GetGameController()
    {
        GameController gameController = GameObject.Find("GameController").GetComponent<GameController>();
        return gameController;
    }
}
