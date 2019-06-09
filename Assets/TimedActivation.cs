using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedActivation : MonoBehaviour
{
    public GameObject objectToActivate;
    // Start is called before the first frame update
    float activationTimer = 0f;
    float timerTarget = 1.5f;

    // Update is called once per frame
    void Update()
    {
        activationTimer += Time.deltaTime;
        if (activationTimer >= timerTarget)
        {
            objectToActivate.SetActive(true);
        }
    }
}
