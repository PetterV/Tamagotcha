using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterNeeds : MonoBehaviour
{
    float messageTimer = 0;
    float messageFrequencyTime = 6f;
    float statIncreaseTimer = 0;
    float statIncreaseTarget = 1f;
    GameController gameController;
    CharacterSpeech characterSpeech;
    PoliticsManager politicsManager;
    Rigidbody rb;
    public GameObject currentPublicAppearance;
    public GameObject currentPolitics;
    public SpriteRenderer dirtSprite;
    public Sprite deathFace;
    public bool isAlive = true;

    public int dirt = 0;
    public int attentionNeed = 200;
    public int poop = 0;
    public int publicStupidity = 100;

    public InteractionSounds interactionSounds;

    [Tooltip("The amount of Dirt gained per second")]
    public int dirtIncrease = 1;
    [Tooltip("The amount of Poop need gained per second")]
    public int poopIncrease = 3;
    [Tooltip("The amount of Public Stupidity Lost per second")]
    public int publicStupidityIncrease = -1;
    [Tooltip("The amount of Attention Need gained per second")]
    public int attentionNeedIncrease = 20;

    [Tooltip("The amount of dirt reduced with each cleaner-click")]
    public int dirtReduction = 50;
    [Tooltip("The force applied when the character is slapped")]
    public float slapForce = 80f;
    [Tooltip("How much need for attention the character loses from Public Appearances")]
    public int attentionNeedReduction = 5;
    [Tooltip("How likely the character is to start making a fool of themselves each PA tick")]
    public int makingFoolChance = 5;

    public bool makingPublicAppearance = false;
    public GameObject publicAppearance;

    public bool makingPolitics = false;
    public GameObject politics;

    bool warnedAboutDirt = false;
    bool warnedAboutPoop = false;
    bool warnedAboutAttentionNeed = false;
    bool warnedAboutPublicStupidity = false;

    public GameObject poopPrefab;

    CursorModes cursor;
    public Image needForAttentionBar;
    public Color lowNeed;
    public Color highNeed;
    public Color dangerousNeed;

    void Start()
    {
        gameController = WorldMethods.GetGameController();
        characterSpeech = gameObject.GetComponent<CharacterSpeech>();
        cursor = WorldMethods.GetCursor();
        politicsManager = WorldMethods.GetPoliticsManager();
        rb = gameObject.GetComponent<Rigidbody>();
        interactionSounds = GameObject.Find("Main Camera").GetComponent<InteractionSounds>();
    }
    // Update is called once per frame
    void Update()
    {
        messageTimer += Time.deltaTime;
        statIncreaseTimer += Time.deltaTime;

        //Increase relevant values
        if (statIncreaseTimer >= statIncreaseTarget && gameController.gameFrozen == false)
        {
            IncreaseDirt();
            IncreasePoop();
            if (!makingPublicAppearance)
            {
                IncreaseAttentionNeed();
            }
            IncreasePublicStupidity();
            
            //Random increases
            if (CheckRandomIncrease())
            {
                IncreaseDirt();
            }
            if (CheckRandomIncrease())
            {
                IncreasePoop();
            }
            if (CheckRandomIncrease())
            {
                IncreaseAttentionNeed();
            }
            if (CheckRandomIncrease())
            {
                IncreasePublicStupidity();
            }

            statIncreaseTimer = 0;
        }

        //Random messages
        if (messageTimer > messageFrequencyTime && !gameController.gameFrozen)
        {
            int roll = gameController.gameRandom.Next(100);
            if (roll < 20)
            {
                characterSpeech.Say("I have very important politics");
            }
            messageTimer = 0;
        }

        //Make sure all values are clamped to 0-1000
        if (dirt < 0)
        {
            dirt = 0;
        }
        if (dirt > 1000)
        {
            dirt = 1000;
        }
        if (poop < 0)
        {
            poop = 0;
        }
        if (poop > 1000)
        {
            poop = 1000;
        }
        if (attentionNeed < 0)
        {
            attentionNeed = 0;
        }
        if (attentionNeed > 1000)
        {
            attentionNeed = 1000;
        }
        if (publicStupidity < 0)
        {
            publicStupidity = 0;
        }
        if (publicStupidity > 1000)
        {
            publicStupidity = 1000;
        }

        //Set DirtSprite alpha appropriately
        float dirtRatio = (float)dirt / 1000f;
        dirtSprite.color = new Color(1, 1, 1, dirtRatio);

        UpdateAttentionNeedBar();
    }

    void UpdateAttentionNeedBar()
    {
        needForAttentionBar.fillAmount = (float)attentionNeed / 1000f;
        if (needForAttentionBar.fillAmount < 0.5f)
        {
            needForAttentionBar.color = Color.Lerp(lowNeed, highNeed, needForAttentionBar.fillAmount * 2);
        }
        else
        {
            needForAttentionBar.color = Color.Lerp(highNeed, dangerousNeed, needForAttentionBar.fillAmount / 2);
        }
    }

    void IncreaseDirt()
    {
        if (politicsManager.numOfPoops < 1)
        {
            dirt += dirtIncrease;
        }
        else
        {
            dirt += dirtIncrease * politicsManager.numOfPoops;
        }
        if (dirt > 800 && !warnedAboutDirt && !characterSpeech.isSpeaking)
        {
            characterSpeech.Say("My suit is getting dirty");
            warnedAboutDirt = true;
        }
    }
    void IncreasePoop()
    {
        poop += poopIncrease;
        if (poop > 900 && !warnedAboutPoop && !characterSpeech.isSpeaking)
        {
            characterSpeech.Say("Uh oh, here comes a poopie");
            warnedAboutPoop = true;
        }

        if (poop >= 1000)
        {
            Poop();
        }
    }
    void Poop()
    {
        GameObject newPoop = Instantiate(poopPrefab);
        newPoop.transform.position = transform.position;
        poop = 0;
        if (makingPublicAppearance)
        {
            politicsManager.ReducePublicConfidence(500);
            interactionSounds.PlayGasps();
        }
        interactionSounds.PlayPoopSound();
    }
    void IncreaseAttentionNeed()
    {
        attentionNeed += attentionNeedIncrease;
        if (attentionNeed > 800 && !warnedAboutAttentionNeed && !characterSpeech.isSpeaking)
        {
            characterSpeech.Say("Why aren't people talking about meee :(");
            warnedAboutAttentionNeed = true;
        }
    }

    public void DecreaseAttentionNeed()
    {
        attentionNeed -= attentionNeedReduction;
    }
    void IncreasePublicStupidity()
    {
        publicStupidity += publicStupidityIncrease;
        if (publicStupidity > 800 && !warnedAboutPublicStupidity && !characterSpeech.isSpeaking)
        {
            characterSpeech.Say("I wonder what people are saying about me on the news");
            warnedAboutPublicStupidity = true;
        }
    }

    bool CheckRandomIncrease()
    {
        bool randomIncrease = false;

        int roll = gameController.gameRandom.Next(101);
        if(roll < 25)
        {
            randomIncrease = true;
        }

        return randomIncrease;
    }

    void OnMouseOver()
    {

        if (Input.GetMouseButtonDown(0) && gameController.gameFrozen == false)
        {
            if (cursor.cleanMode)
            {
                CleanUp();
            }
            if (cursor.slapMode)
            {
                Slap();
            }
            if (cursor.publicAppearanceMode &! DoingSomething())
            {
                MakePublicAppearance();
            }
            else if (cursor.publicAppearanceMode)
            {
                interactionSounds.PlayEmptyClick();
            }
            if (cursor.politicsMode &! DoingSomething())
            {
                MakePolitics();
            }
            else if (cursor.politicsMode)
            {
                interactionSounds.PlayEmptyClick();
            }
        }
        
    }

    bool DoingSomething()
    {
        bool doingSomething = false;
        if (makingPolitics)
        {
            doingSomething = true;
        }
        if (makingPublicAppearance)
        {
            doingSomething = true;
        }
        return doingSomething;
    }

    void CleanUp()
    {
        dirt -= dirtReduction;
        if (dirt < 0)
        {
            dirt = 0;
            interactionSounds.PlayEmptyClick();
        }
        else
        {
            interactionSounds.PlayCleanSound();
        }
    }

    void Slap()
    {
        if (makingPublicAppearance)
        {
            StopPublicAppearance();
        }
        else if (makingPolitics)
        {
            StopPolitics();
        }
        //TODO: Redo physics to be less floaty
        Vector3 newDirectionVector = Random.onUnitSphere;
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.AddForce(newDirectionVector * slapForce, ForceMode.Impulse);
        interactionSounds.PlaySlapSound();
    }

    public void MakePublicAppearance()
    {
        makingPublicAppearance = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        GameObject newAppearance = Instantiate(publicAppearance);
        newAppearance.GetComponent<PublicAppearance>().character = gameObject;
        Vector3 newLocation = new Vector3(transform.position.x- 1.5f, transform.position.y - 0.1f, transform.position.z-0.2f);
        newAppearance.transform.position = newLocation;
        currentPublicAppearance = newAppearance;
        interactionSounds.PlayPublicAppearanceSound();
    }
    public void StopPublicAppearance()
    {
        makingPublicAppearance = false;
        Destroy(currentPublicAppearance);
    }

    public void MakePolitics()
    {
        makingPolitics = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        GameObject newPolitics = Instantiate(politics);
        newPolitics.GetComponent<Politics>().character = gameObject;
        newPolitics.transform.position = gameObject.transform.position;
        currentPolitics = newPolitics;
        interactionSounds.PlayPoliticsSound();
    }

    public void StopPolitics()
    {
        makingPolitics = false;
        Destroy(currentPolitics);
    }
}
