using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public GameObject robotBase, link1, link2, link3, link4, link5, tool;

    [Range(0.0f, 360.0f)]
    public float q1;
    [Range(0.0f, 360.0f)]
    public float q2;
    [Range(0.0f, 360.0f)]
    public float q3;
    [Range(0.0f, 360.0f)]
    public float q4;
    [Range(0.0f, 360.0f)]
    public float q5;
    [Range(0.0f, 360.0f)]
    public float q6;


    JointSpring baseSpring, link1Spring, link2Spring, link3Spring, link4Spring, link5Spring;
    HingeJoint baseJoint, link1Joint, link2Joint, link3Joint, link4Joint, link5Joint;

    void Start()
    {
        baseJoint = robotBase.GetComponent<HingeJoint>();
        baseSpring = baseJoint.spring;

        link1Joint = link1.GetComponent<HingeJoint>();
        link1Spring = link1Joint.spring;

        link2Joint = link2.GetComponent<HingeJoint>();
        link2Spring = link2Joint.spring;

        link3Joint = link3.GetComponent<HingeJoint>();
        link3Spring = link3Joint.spring;

        link4Joint = link4.GetComponent<HingeJoint>();
        link4Spring = link4Joint.spring;

        link5Joint = link5.GetComponent<HingeJoint>();
        link5Spring = link5Joint.spring;

    }
    void Update()
    {
        baseSpring.targetPosition = q1;
        baseJoint.spring = baseSpring;

        link1Spring.targetPosition = q2;
        link1Joint.spring = link1Spring;

        link2Spring.targetPosition = q3;
        link2Joint.spring = link2Spring;

        link3Spring.targetPosition = q4;
        link3Joint.spring = link3Spring;

        link4Spring.targetPosition = q5;
        link4Joint.spring = link4Spring;

        link5Spring.targetPosition = q6;
        link5Joint.spring = link5Spring;

    }
}
