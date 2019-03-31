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

    public static CursorModes GetCursor()
    {
        CursorModes cursor = GameObject.Find("CursorManager").GetComponent<CursorModes>();
        return cursor;
    }

    public static PoliticsManager GetPoliticsManager()
    {
        PoliticsManager politicsManager = GameObject.Find("PoliticsManager").GetComponent<PoliticsManager>();
        return politicsManager;
    }
}
