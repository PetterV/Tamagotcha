using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSounds : MonoBehaviour
{
    GameController gameController;
    
    public AudioSource poopSource;
    public AudioSource emptyClickAudioSource;
    public AudioSource gaspsAudioSource;
    public AudioSource politicsAudioSource;
    public AudioSource publicAppearanceAudioSource;
    public AudioSource cleanUpAudioSource;
    public AudioSource slapAudioSource;

    public float soundTimer = 0f;
    public bool playingSound = false;
    public float soundLength;

    void Start()
    {
        gameController = WorldMethods.GetGameController();
    }
    
    public void PlayCleanSound()
    {
        cleanUpAudioSource.Play();
    }
    public void PlaySlapSound()
    {
        slapAudioSource.Play();
    }
    public void PlayPoliticsSound()
    {
       politicsAudioSource.Play();
    }
    public void PlayPublicAppearanceSound()
    {
        publicAppearanceAudioSource.Play();
    }

    public void PlayEmptyClick()
    {
        emptyClickAudioSource.Play();
    }

    public void PlayPoopSound()
    {
        poopSource.Play();
    }

    public void PlayGasps()
    {
        gaspsAudioSource.Play();
    }
}
