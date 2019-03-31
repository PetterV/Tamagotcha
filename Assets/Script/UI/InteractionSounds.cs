using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSounds : MonoBehaviour
{
    GameController gameController;

    public AudioSource source;
    public AudioSource poopSource;
    public AudioSource emptyClickAudioSource;
    public AudioSource gaspsAudioSource;
    public AudioClip cleanSound;
    public AudioClip slapSound;
    public AudioClip publicAppearanceSound;
    public AudioClip politicsSound;
    public AudioClip emptyClickSound;

    public float soundTimer = 0f;
    public bool playingSound = false;
    public float soundLength;

    void Start()
    {
        gameController = WorldMethods.GetGameController();
    }
    void Update()
    {
        if (playingSound)
        {
            soundTimer += Time.deltaTime;
            if (soundTimer >= soundLength)
            {
                playingSound = false;
                soundTimer = 0f;
            }
        }
    }

    public void PlayCleanSound()
    {
        if (!playingSound || source.clip != cleanSound)
        {
            source.clip = cleanSound;
            source.Play();
            playingSound = true;
            soundLength = cleanSound.length;
        }
    }
    public void PlaySlapSound()
    {
        if (!playingSound || source.clip != slapSound)
        {
            source.clip = slapSound;
            source.Play();
            playingSound = true;
            soundLength = 0.25f; //Can be overriden after .25 seconds
        }
    }
    public void PlayPoliticsSound()
    {
        if (!playingSound || source.clip != politicsSound)
        {
            source.clip = politicsSound;
            source.Play();
            playingSound = true;
            soundLength = 0.1f; //Can be overriden after .1 seconds
        }
    }
    public void PlayPublicAppearanceSound()
    {
        if (!playingSound || source.clip != publicAppearanceSound)
        {
            source.clip = publicAppearanceSound;
            source.Play();
            playingSound = true;
            soundLength = 0.1f; //Can be overriden after .1 seconds
        }
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
