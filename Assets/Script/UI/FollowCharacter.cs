using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCharacter : MonoBehaviour
{
    public GameObject characterToFollow;

    // Update is called once per frame
    void Update()
    {
        transform.position = characterToFollow.transform.position;
    }
}
