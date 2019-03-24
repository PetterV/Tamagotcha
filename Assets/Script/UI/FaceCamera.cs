using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    Vector3 cameraFacing;

    void Start()
    {
        cameraFacing = transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.rotation.eulerAngles != cameraFacing)
        {
            transform.eulerAngles = cameraFacing;
        }
    }
}
