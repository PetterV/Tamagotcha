using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterNeeds : MonoBehaviour
{
    float messageTimer;
    float messageFrequencyTime = 6f;
    GameController gameController;

    void Start()
    {
        gameController = WorldMethods.GetGameController();
    }
    // Update is called once per frame
    void Update()
    {
        messageTimer += Time.deltaTime;

        if (messageTimer > messageFrequencyTime)
        {
            int roll = gameController.gameRandom.Next(100);
            if (roll > 20)
            {
                gameObject.GetComponent<CharacterSpeech>().Say("I have very important politics");
            }
            else
            {
                gameObject.GetComponent<CharacterSpeech>().Say("Uh-oh, here comes a poopie");
            }
            messageTimer = 0;
        }
    }
}
