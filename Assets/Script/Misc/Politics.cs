using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Politics : MonoBehaviour
{
    public GameObject character;
    CharacterNeeds characterNeeds;
    GameController gameController;
    PoliticsManager politicsManager;
    public Image progressBar;

    float timer;
    float lifeTime;
    public float politicsDuration = 5f;

    public int politicsGainBase = 15;

    public int attentionNeedGain = 5;

    // Start is called before the first frame update
    void Start()
    {
        characterNeeds = character.GetComponent<CharacterNeeds>();
        gameController = WorldMethods.GetGameController();
        politicsManager = WorldMethods.GetPoliticsManager();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        lifeTime += Time.deltaTime;

        if (timer >= 0.2f)
        {
            PoliticsTick();
            timer = 0f;
        }

        if (lifeTime >= politicsDuration)
        {
            characterNeeds.StopPolitics();
        }

        progressBar.fillAmount = lifeTime / politicsDuration;
    }

    void PoliticsTick()
    {
        politicsManager.IncreasePolitics(PoliticsGain(politicsGainBase));
        characterNeeds.attentionNeed = characterNeeds.attentionNeed + attentionNeedGain;
    }

    int PoliticsGain(int basePolitics)
    {
        float politicsToGainFloat = basePolitics * 1000;
        float politicsToSubtract = basePolitics * characterNeeds.dirt;
        politicsToGainFloat = politicsToGainFloat - politicsToSubtract;
        politicsToGainFloat = politicsToGainFloat / 1000;
        int politicsToGain = (int)Mathf.Round(politicsToGainFloat);
        return politicsToGain;
    }
}
