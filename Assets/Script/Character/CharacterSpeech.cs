using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSpeech : MonoBehaviour
{
    public GameObject myCanvas;
    Image speechBubble;
    TextMeshProUGUI speechText;

    bool isSpeaking = false;

    float speechTimer;
    float speechDuration;

    void Start()
    {
        speechBubble = myCanvas.GetComponentInChildren<Image>();
        speechBubble.enabled = false;
        speechText = myCanvas.GetComponentInChildren<TextMeshProUGUI>();
        speechText.enabled = false;
    }

    public void Say(string thingToSay, float duration = 4.0f)
    {
        speechBubble.enabled = true;
        speechText.text = thingToSay;
        speechText.enabled = true;
        isSpeaking = true;
        speechTimer = 0f;
        speechDuration = duration;
    }

    void Update()
    {
        speechTimer += Time.deltaTime;

        if(speechTimer > speechDuration)
        {
            isSpeaking = false;
            speechBubble.enabled = false;
            speechText.enabled = false;
        }
    }
}
