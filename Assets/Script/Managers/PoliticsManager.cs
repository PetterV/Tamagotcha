using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoliticsManager : MonoBehaviour
{
    GameController gameController;
    public int politics = 0;
    public int publicConfidence = 10000;

    public int publicConfidenceLossPerTick = 5;

    public Image politicsBar;
    public Image publicConfidenceBar;

    public int numOfPoops = 0;

    float timer;

    public GameObject[] allCharacters;

    public GameObject failurePanel;
    public GameObject successPanel;
    public bool gameDone = false;
    public bool won = false;
    public bool lost = false;

    void Start()
    {
        gameController = WorldMethods.GetGameController();
        allCharacters = GameObject.FindGameObjectsWithTag("Character");
    }
    // Update is called once per frame
    void Update()
    {
        if (publicConfidence <= 0)
        {
            Lose();
        }
        else if (politics >= 10000)
        {
            Win();
        }
        if (!gameDone && !gameController.gameFrozen)
        {
            timer += Time.deltaTime;

            if (timer >= 0.5f)
            {
                ReducePublicConfidence(PublicConfidenceToLose(publicConfidenceLossPerTick));
                timer = 0f;
            }

            politicsBar.fillAmount = (float)politics / 10000f;
            publicConfidenceBar.fillAmount = (float)publicConfidence / 10000f;
        }
    }

    public void ReducePublicConfidence(int loss)
    {
        float finalLossFloat = loss * CalculateDirtModifier();
        int finalLoss = (int)Mathf.Round(finalLossFloat);
        publicConfidence -= finalLoss;
    }

    public void IncreasePublicConfidence(int gain)
    {
        publicConfidence += gain;
    }

    public void ReducePolitics(int loss)
    {
        politics -= loss;
    }

    public void IncreasePolitics(int gain)
    {
        politics += gain;
    }

    int PublicConfidenceToLose(int value)
    {
        int confidenceToLose = value;
        //TODO: Calculate based on an average-Dirt modifier
        float confidenceToLoseFloat = confidenceToLose * CalculateDirtModifier();
        confidenceToLose = (int)Mathf.Round(confidenceToLoseFloat);
        return confidenceToLose;
    }
    
    public float CalculateDirtModifier()
    {
        float dirtModifier = 1;
        int totalDirt = 0;
        foreach (GameObject c in allCharacters)
        {
            CharacterNeeds needsScript = c.transform.GetComponentInChildren<CharacterNeeds>();
            int dirt = needsScript.dirt;
            totalDirt += dirt;
        }
        dirtModifier += (float)totalDirt / 1000f;
        //Reduce immediate impact of multiple high-dirt characters
        if (dirtModifier > 2.5f && allCharacters.Length >= 3)
        {
            dirtModifier = dirtModifier * 0.8f;
        }
        //float averageDirt = totalDirt / allCharacters.Length;

        return dirtModifier;
    }

    public void Lose()
    {
        gameDone = true;
        GameController gameController = WorldMethods.GetGameController();
        gameController.gameFrozen = true;
        lost = true;
        failurePanel.SetActive(true);
        GameObject.Find("Main Camera").GetComponent<InteractionSounds>().PlayGasps();
    }

    public void Win()
    {
        gameDone = true;
        gameController.gameFrozen = true;
        won = true;
        successPanel.SetActive(true);
        GameObject.Find("Main Camera").GetComponent<InteractionSounds>().PlayPoopSound();
        GameObject.Find("Main Camera").GetComponent<InteractionSounds>().PlayPoliticsSound();
    }
}
