using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poop : MonoBehaviour
{
    public int startingPoopHealth = 7;
    public int poopHealth;
    CursorModes cursor;
    Vector3 startScale;
    PoliticsManager politicsManager;

    void Start()
    {
        poopHealth = startingPoopHealth;
        cursor = WorldMethods.GetCursor();
        startScale = transform.localScale;
        politicsManager = WorldMethods.GetPoliticsManager();
        politicsManager.numOfPoops += 1;
    }
    public void ReducePoopHealth()
    {
        poopHealth -= 1;
        CheckForDestruction();
        float currentHealthRatio = (float)poopHealth / (float)startingPoopHealth;
        transform.localScale = new Vector3(startScale.x, startScale.y * currentHealthRatio);
    }

    void CheckForDestruction()
    {
        if (poopHealth <= 0)
        {
            politicsManager.numOfPoops -= 1;
            Destroy(gameObject);
        }
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(0) && cursor.cleanMode)
        {
            ReducePoopHealth();
        }
    }
}
