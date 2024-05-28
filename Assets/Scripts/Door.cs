using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private HingeJoint doorHinge;
    JointMotor motor;
    private float velocity;
    private float angle;
    void Start()
    {
        doorHinge = gameObject.GetComponent<HingeJoint>();
        motor = doorHinge.motor;
    }

    
    void Update()
    {
        angle = doorHinge.angle;
        motor.targetVelocity = -angle;
        doorHinge.motor = motor;
    }
}
