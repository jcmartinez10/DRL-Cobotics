using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorquedMovement : MonoBehaviour {

    public GameObject robotBase, link1, link2, link3, link4, link5, tool;

    [Range(-180.0f, 180.0f)]
    public float q1;
    [Range(-180f, 180.0f)]
    public float q2;
    [Range(-180f, 180.0f)]
    public float q3;
    [Range(-180f, 180.0f)]
    public float q4;
    [Range(-180f, 180.0f)]
    public float q5;
    [Range(-180f, 180.0f)]
    public float q6;


    JointMotor baseMotor, link1Motor, link2Motor, link3Motor, link4Motor, link5Motor;
    HingeJoint baseJoint, link1Joint, link2Joint, link3Joint, link4Joint, link5Joint;

    void Start()
    {
        baseJoint = robotBase.GetComponent<HingeJoint>();
        baseMotor = baseJoint.motor;
        baseJoint.useMotor = true;
        baseJoint.useSpring = false;

        link1Joint = link1.GetComponent<HingeJoint>();
        link1Motor = link1Joint.motor;
        link1Joint.useMotor = true;
        link1Joint.useSpring = false;

        link2Joint = link2.GetComponent<HingeJoint>();
        link2Motor = link2Joint.motor;
        link2Joint.useMotor = true;
        link2Joint.useSpring = false;

        link3Joint = link3.GetComponent<HingeJoint>();
        link3Motor = link3Joint.motor;
        link3Joint.useMotor = true;
        link3Joint.useSpring = false;

        link4Joint = link4.GetComponent<HingeJoint>();
        link4Motor = link4Joint.motor;
        link4Joint.useMotor = true;
        link4Joint.useSpring = false;

        link5Joint = link5.GetComponent<HingeJoint>();
        link5Motor = link5Joint.motor;
        link5Joint.useMotor = true;
        link5Joint.useSpring = false;

    }
    void Update()
    {

        ReachPosition(baseJoint, baseMotor, q1);
        ReachPosition(link1Joint, link1Motor, q2);
        ReachPosition(link2Joint, link2Motor, -q3);
        ReachPosition(link3Joint, link3Motor, q4);
        ReachPosition(link4Joint, link4Motor, q5);
        ReachPosition(link5Joint, link5Motor, q6);
    }

    void ReachPosition(HingeJoint pJoint, JointMotor pMotor, float q)
    {
        if ((q-pJoint.angle) > 1)
        {
            pMotor.force = 10000;
            if ((q - pJoint.angle) > 10){
                pMotor.targetVelocity = 90;
            }
            else{
                pMotor.targetVelocity = 10;
            }
            pMotor.freeSpin = false;
            pJoint.motor = pMotor;
            //pMotor.useMotor = true;
        }
        else if ((pJoint.angle - q) > 0.5)
        {
            pMotor.force = 10000;
            if ((pJoint.angle - q) > 10)
            {
                pMotor.targetVelocity = -90;
            }
            else
            {
                pMotor.targetVelocity = -10;
            }
            pMotor.freeSpin = false;
            pJoint.motor = pMotor;
            //pMotor.useMotor = true;
        }
        else
        {
            pMotor.force = 10000;
            pMotor.targetVelocity = 0;
            pMotor.freeSpin = false;
            pJoint.motor = pMotor;
            //pMotor.useMotor = true;
        }


    }


}
