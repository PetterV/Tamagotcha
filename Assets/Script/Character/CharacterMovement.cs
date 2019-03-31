using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float movementForce = 2000f;
    public int movementFrequency = 50;
    public float movementCooldown = 2.5f;
    public bool readyToMove = false;
    float movementTimer = 0;

    GameController gameController;

    public GameObject characterSprite;

    // Start is called before the first frame update
    void Start()
    {
        gameController = WorldMethods.GetGameController();
        characterSprite = gameObject.transform.parent.Find("CharacterSprite").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
        movementTimer += Time.deltaTime;

        if (movementTimer > movementCooldown && !gameController.gameFrozen)
        {
            readyToMove = true;
        }

        if (readyToMove && !gameController.gameFrozen)
        {
            if (!gameObject.GetComponent<CharacterNeeds>().makingPublicAppearance && !gameObject.GetComponent<CharacterNeeds>().makingPolitics)
            {
                int movementRoll = gameController.gameRandom.Next(100);
                if (movementRoll < movementFrequency)
                {
                    //Move

                    //TODO: Make another random roll for whether they change direction at all
                    PickMovementDirection();
                    MoveForward(movementForce);
                }
            }
            //Reset for next roll
            readyToMove = false;
            movementTimer = 0f;
        }
    }

    void PickMovementDirection()
    {
        float newRotation = (float)gameController.gameRandom.Next(361);
        Vector3 newDirectionVector = new Vector3(transform.rotation.x, newRotation, transform.rotation.z);
        transform.localEulerAngles = newDirectionVector;
    }

    void MoveForward(float force)
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * force);
    }
}
