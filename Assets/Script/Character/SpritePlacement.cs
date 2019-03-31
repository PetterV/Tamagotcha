using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritePlacement : MonoBehaviour
{
    Transform location;
    void Start()
    {
        location = gameObject.transform.parent.Find("Character");
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = location.position;
    }
}
