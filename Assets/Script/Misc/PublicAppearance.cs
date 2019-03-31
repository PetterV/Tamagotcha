using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicAppearance : MonoBehaviour
{
    public GameObject character;
    CharacterNeeds characterNeeds;
    GameController gameController;
    PoliticsManager politicsManager;
    GameObject warningSign;

    float timer;

    public bool makingFool = false;
    public int confidenceLossBase = 2;
    public int confidenceGainBase = 10;
    public int politicsGainBase = 2;
    
    // Start is called before the first frame update
    void Start()
    {
        characterNeeds = character.GetComponent<CharacterNeeds>();
        gameController = WorldMethods.GetGameController();
        politicsManager = WorldMethods.GetPoliticsManager();
        warningSign = transform.Find("WarningSign").gameObject;
        warningSign.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        
        if (timer >= 0.25f)
        {
            PublicAppearanceTick();
            timer = 0f;
        }
    }

    void PublicAppearanceTick()
    {
        characterNeeds.DecreaseAttentionNeed();
        if (!makingFool)
        {
            politicsManager.IncreasePolitics(PoliticsGainValue());

            if (RollForFoolishness())
            {
                makingFool = true;
                warningSign.SetActive(true);
                GameObject.Find("Main Camera").GetComponent<InteractionSounds>().PlayGasps();
            }
        }
        else
        {
            politicsManager.ReducePublicConfidence(ConfidenceLossValue());
        }
    }

    bool RollForFoolishness()
    {
        bool becomeFoolish = false;
        int roll = gameController.gameRandom.Next(101);

        if (roll < characterNeeds.makingFoolChance)
        {
            becomeFoolish = true;
        }

        return becomeFoolish;
    }

    int ConfidenceLossValue()
    {
        float confidenceValueFloat = confidenceLossBase * characterNeeds.dirt;
        confidenceValueFloat = confidenceValueFloat / 1000f;
        confidenceValueFloat = confidenceValueFloat + confidenceLossBase;
        int confidenceValue = (int)confidenceValueFloat;

        return confidenceValue;
    }

    int ConfidenceGainValue()
    {
        float confidenceValueFloat = confidenceLossBase * characterNeeds.dirt;
        confidenceValueFloat = confidenceValueFloat / 1000f;
        confidenceValueFloat = confidenceGainBase - confidenceValueFloat;
        int confidenceValue = (int)Mathf.Round(confidenceValueFloat);

        return confidenceValue;
    }

    int PoliticsGainValue()
    {
        float politicsValueFloat = politicsGainBase * characterNeeds.dirt;
        politicsValueFloat = politicsValueFloat / 1000f;
        politicsValueFloat = politicsGainBase - politicsValueFloat;
        int politicsValue = (int)politicsValueFloat;

        return politicsValue;
    }
}
