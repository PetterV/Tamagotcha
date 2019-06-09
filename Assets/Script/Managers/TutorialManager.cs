using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public void OpenNext(GameObject thisOne, GameObject next)
    {
        next.SetActive(true);
        thisOne.SetActive(false);
    }
}
