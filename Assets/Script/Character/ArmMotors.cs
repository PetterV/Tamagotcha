using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmMotors : MonoBehaviour
{
    HingeJoint2D joint;
    // Start is called before the first frame update
    void Start()
    {
        joint = gameObject.GetComponent<HingeJoint2D>();
    }

    public void MoveLimb()
    {
        joint.useMotor = true;
    }
    public void StopLimb()
    {
        joint.useMotor = false;
    }
}
